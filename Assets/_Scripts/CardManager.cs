using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //手牌实例,出牌实例
    //手牌点击功能实现,能否出牌判断
    //手牌,出牌属性及显示

    //卡牌信息存储
    public CounterCard counterCard;
    public AttackCard attackCard;

    //图片UI
    public Image attackImage;
    public Image attackBallImage;
    public Image counterImage;
    public Image counterBallImage;
    public Image damageCubeImage;

    //行动点和伤害UI
    public Text attackPointText;
    public Text counterPointText;
    public Text damageText;

    //公共介绍框UI
    public IntroductionManager introductionManager;

    //点击事件
    public HandCardButton cardButton;//产生何种按钮，不影响功能
    public GameObject attackButton;
    public GameObject counterButton;

    //手牌功能
    public int handCardNo;//手牌号

    //出牌类
    public ShowingCard showingCard;

    //玩家类
    public PlayerManager myPlayer;

    //手牌类
    public CardCurved cardCurved;

    public GameObject apNotEnough;//AP不足提示
    public Transform mainCanvas;//主画布

    private RoundManager roundManager;

    // Update is called once per frame
    void Update()
    {

    }

    //卡面载入，介绍框获取
    public void init()
    {
        //A卡面载入
        attackImage.sprite = attackCard.attackSprite;
        attackBallImage.sprite = attackCard.attackBallSprite;
        damageCubeImage.sprite = attackCard.damageCubeSprite;
        attackPointText.text = attackCard.ActionPoint + "";
        damageText.text = attackCard.Damage + "";

        //C卡面载入
        counterImage.sprite = counterCard.counterSprite;
        counterBallImage.sprite = counterCard.counterBallSprite;
        counterPointText.text = counterCard.ActionPoint + "";

        //物体获取
        introductionManager = GameObject.Find("Introduction").GetComponent<IntroductionManager>();
        showingCard = GameObject.Find("ShowCardParent").GetComponent<ShowingCard>();
        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        mainCanvas = GameObject.Find("Canvas").transform;
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入显示卡名
        if (introductionManager.handCardNo != handCardNo)
        {
            introductionManager.attackCard = new AttackCard(attackCard);
            introductionManager.counterCard = new CounterCard(counterCard);
            introductionManager.UpdateForDisplay();
        }
        introductionManager.DisplayIntroduction();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开范围
        if (eventData.IsPointerMoving())
            introductionManager.HiddenIntroduction();
    }

    //点击事件
    public void ButtonInstantiate()
    {
        if (!cardCurved.isAbandonment && !showingCard.delayAttack)
        {
            //使用牌

            //删除之前按钮
            Destroy(GameObject.Find("AttackButton(Clone)"));
            Destroy(GameObject.Find("CounterButton(Clone)"));

            //根据分辨率计算位置
            float local_x = Input.mousePosition.x / (float)Screen.width * 1280f;
            float local_y = Input.mousePosition.y / (float)Screen.height * 720f;

            //根据情况生成按钮
            GameObject GOCardButton = null;
            if (roundManager.roundPhase == RoundPhase.Main
                && roundManager.isMyturn
                && roundManager.waitCounter == WaitPhase.NoWait)
            {
                //我方回合进攻
                GOCardButton = Instantiate(attackButton) as GameObject;
                GOCardButton.transform.position = new Vector3(local_x, local_y, 0);
                GOCardButton.transform.parent = transform;
                GOCardButton.GetComponent<ActionButton>().cardManager = this;
            }
            else if (roundManager.waitCounter == WaitPhase.WaitMe)
            {
                //对方回合反击
                GOCardButton = Instantiate(counterButton) as GameObject;
                GOCardButton.transform.position = new Vector3(local_x, local_y, 0);
                GOCardButton.transform.parent = transform;
                GOCardButton.GetComponent<ActionButton>().cardManager = this;
            }
        }
        else
        {
            //弃牌

            if (roundManager.roundPhase == RoundPhase.Abandonment && roundManager.isMyturn)
            {
                //处于我方弃牌阶段，保留5张，判断是否还需弃牌
                cardCurved.DestroyTheCard(handCardNo);
                cardCurved.AbandonmentCard();
            }
            else if (showingCard.delayAttack)
            {
                //流星雨弃牌中，最大弃3张，每张伤害+1
                GameObject ballfireGO = GameObject.Find("FireBallOver(Clone)");
                if (ballfireGO.GetComponent<FireBallFire>().addDamage < 3)
                {
                    cardCurved.DestroyTheCard(handCardNo);
                    ballfireGO.GetComponent<FireBallFire>().AddtheDamage();
                }
            }
        }
    }

    public void UseCard()
    {
        //使用卡牌

        //判断AP是否够

        if ((roundManager.isMyturn && 
            myPlayer.IsApEnough(attackCard.ActionPoint)) || 
            (!roundManager.isMyturn && 
            myPlayer.IsApEnough(counterCard.ActionPoint)))
        {
            //开启动画协程
            StartCoroutine(InstantiateShowCard());

            //删除卡牌
            cardCurved.DestroyTheCard(handCardNo);
        }
        else
        {
            //显示AP不足
            Instantiate(apNotEnough, mainCanvas);
        }
    }

    IEnumerator InstantiateShowCard()
    {
        //出牌动画协程

        //赋值
        showingCard.attackCard = new AttackCard(attackCard);
        showingCard.counterCard = new CounterCard(counterCard);

        //出牌
        showingCard.InstantiateInit();
        yield return null;
    }
}

//卡种记号
public enum HandCardButton
{
    Attack,
    Counter,
    Hero,
    Awake,
    Cannot
}
