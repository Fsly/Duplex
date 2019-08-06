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

    
    public GameObject apNotEnough;//AP不足提示
    public Transform mainCanvas;//主画布

    // Update is called once per frame
    void Update()
    {

    }

    //卡面载入，介绍框获取
    public void init()
    {
        //卡面载入
        attackImage.sprite = attackCard.attackSprite;
        attackBallImage.sprite = attackCard.attackBallSprite;
        damageCubeImage.sprite = attackCard.damageCubeSprite;
        attackPointText.text = attackCard.ActionPoint + "";
        damageText.text = attackCard.Damage + "";

        counterImage.sprite = counterCard.counterSprite;
        counterBallImage.sprite = counterCard.counterBallSprite;
        counterPointText.text = counterCard.ActionPoint + "";

        //介绍框获取，出牌类获取，我方玩家获取
        introductionManager = GameObject.Find("Introduction").GetComponent<IntroductionManager>();
        showingCard = GameObject.Find("ShowCardParent").GetComponent<ShowingCard>();
        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        mainCanvas = GameObject.Find("Canvas").transform;
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

    public void ButtonInstantiate()
    {
        //点击卡牌根据情况生成按钮
        GameObject GOCardButton = null;
        if (cardButton == HandCardButton.Attack)
        {
            GOCardButton = Instantiate(attackButton) as GameObject;
        }
        else if (cardButton == HandCardButton.Counter)
        {
            GOCardButton = Instantiate(counterButton) as GameObject;
        }
        GOCardButton.transform.position = Input.mousePosition;
        GOCardButton.transform.parent = transform;
        GOCardButton.GetComponent<ActionButton>().cardManager = this;
    }

    public void UseCard()
    {
        //使用卡牌

        //判断AP是否够
        if (myPlayer.IsApEnough(attackCard.ActionPoint))
        {
            //开启动画协程
            StartCoroutine(InstantiateShowCard());

            //删除卡牌
            GameObject.Find("HandCardPrefab").GetComponent<CardCurved>().DestroyTheCard(handCardNo);
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
