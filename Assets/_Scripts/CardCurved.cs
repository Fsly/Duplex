using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardCurved : MonoBehaviour
{
    //手牌管理
    //实现手牌区域的曲面化以及DOTween实现旋转
    //手牌储存，抽牌

    public GameObject[] YGO_Card; //代替用牌

    public AttackCard[] attackCards; //进攻牌
    public CounterCard[] counterCards; //反击牌

    public GameObject NextCard; //下一张手牌
    public int NextCardNo;//下一张手牌的手牌号
    public int AcardNo;  //下一张手牌的进攻
    public int CcardNo;  //下一张手牌的反击

    public List<GameObject> ListHandCard = new List<GameObject>(); //手牌列表 
    public GameObject TransBeginHandCard;  //生成手牌最开始的位置
    public float _FloRotateAngel; //手牌动画旋转的角度

    public bool isAbandonment;//是否弃牌

    private RoundManager roundManager;

    // Use this for initialization
    void Start()
    {
        NextCardNo = 0;
        isAbandonment = false;

        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //响应按键，待改
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //抽牌

            //YGO测试 YGOHandGeneration();

            GetCards();
            AddCardAnimations();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //出最后那张牌
            DestroyLastCard();
            UseCardAnimation();
        }
    }



    public void GetCards()
    {
        //克隆下一张手牌
        GameObject GOHandCard = Instantiate(NextCard) as GameObject;
        GOHandCard.transform.position = TransBeginHandCard.transform.position;
        GOHandCard.transform.parent = transform;

        //为手牌随机赋值并初始化
        RandomGeneration();
        GOHandCard.GetComponent<CardManager>().attackCard = new AttackCard(attackCards[AcardNo]);
        GOHandCard.GetComponent<CardManager>().counterCard = new CounterCard(counterCards[CcardNo]);
        GOHandCard.GetComponent<CardManager>().init();

        //为此牌编号
        GOHandCard.GetComponent<CardManager>().handCardNo = NextCardNo;
        NextCardNo++;

        //将新手牌添加到手牌列表
        ListHandCard.Add(GOHandCard);

        //计算动画需要旋转的角度
        RotateAngel();

    }

    //为手牌添加动画
    public void HandCardAnimation(GameObject GO, float Vec3_Z)
    {
        GO.transform.DORotate(new Vector3(0, 0, Vec3_Z), 0.3F, RotateMode.Fast);
    }

    //增加手牌时播放的动画
    public void AddCardAnimations()
    {
        if (ListHandCard.Count == 1)
        {
            HandCardAnimation(ListHandCard[0], 0);
        }
        else
        {
            for (int i = 1; i < ListHandCard.Count; i++)
            {
                HandCardAnimation(ListHandCard[i - 1], 30 - _FloRotateAngel * i * ListHandCard.Count + 2.5F);
            }
            HandCardAnimation(ListHandCard[ListHandCard.Count - 1], -27.5F + _FloRotateAngel);
        }
    }

    //使用手牌时播放的动画
    public void UseCardAnimation()
    {
        if (ListHandCard.Count == 1)
        {
            HandCardAnimation(ListHandCard[0], 0);
        }
        else if (ListHandCard.Count > 1)
        {
            for (int i = 1; i < ListHandCard.Count + 1; i++)
            {
                HandCardAnimation(ListHandCard[i - 1], 30 - _FloRotateAngel * i * ListHandCard.Count + 2.5F);
                HandCardAnimation(ListHandCard[ListHandCard.Count - 1], -27.5F + _FloRotateAngel);
            }
        }
    }

    //删除最后那张牌
    public void DestroyLastCard()
    {
        Destroy(ListHandCard[0]);

        //将删除的手牌从列表移除
        ListHandCard.Remove(ListHandCard[0]);

        //计算动画需要旋转的角度
        RotateAngel();
    }

    //计算需要旋转的角度
    private void RotateAngel()
    {
        _FloRotateAngel = 55F / ListHandCard.Count / ListHandCard.Count;
    }

    //随机生成一张牌
    public void RandomGeneration()
    {
        AcardNo = Random.Range(0, attackCards.Length);
        CcardNo = Random.Range(0, counterCards.Length);
    }

    //改变卡的攻击面
    public void attackAssignment(GameObject card1, AttackCard card2)
    {
        card1.GetComponent<CardManager>().attackCard.attackCardNo = card2.attackCardNo;
        card1.GetComponent<CardManager>().attackCard.CardName = card2.CardName;
        card1.GetComponent<CardManager>().attackCard.CardIntroduction = card2.CardIntroduction;
        card1.GetComponent<CardManager>().attackCard.CardEffect = card2.CardEffect;
        card1.GetComponent<CardManager>().attackCard.counterAttribute = card2.counterAttribute;
        card1.GetComponent<CardManager>().attackCard.ActionPoint = card2.ActionPoint;
        card1.GetComponent<CardManager>().attackCard.damageAttribute = card2.damageAttribute;
        card1.GetComponent<CardManager>().attackCard.Damage = card2.Damage;
        card1.GetComponent<CardManager>().attackCard.attackSprite = card2.attackSprite;
        card1.GetComponent<CardManager>().attackCard.attackBallSprite = card2.attackBallSprite;
        card1.GetComponent<CardManager>().attackCard.damageCubeSprite = card2.damageCubeSprite;
    }

    //改变卡的反击面
    public void counterAssignment(GameObject card1, CounterCard card2)
    {
        card1.GetComponent<CardManager>().counterCard.counterCardNo = card2.counterCardNo;
        card1.GetComponent<CardManager>().counterCard.CardName = card2.CardName;
        card1.GetComponent<CardManager>().counterCard.CardIntroduction = card2.CardIntroduction;
        card1.GetComponent<CardManager>().counterCard.CardEffect = card2.CardEffect;
        card1.GetComponent<CardManager>().counterCard.counterAttribute = card2.counterAttribute;
        card1.GetComponent<CardManager>().counterCard.ActionPoint = card2.ActionPoint;
        card1.GetComponent<CardManager>().counterCard.counterSprite = card2.counterSprite;
        card1.GetComponent<CardManager>().counterCard.counterBallSprite = card2.counterBallSprite;
    }

    //移除指定牌
    public void DestroyTheCard(int DesCardNo)
    {

        for (int i = 0; i < ListHandCard.Count; i++)
        {
            if (ListHandCard[i].GetComponent<CardManager>().handCardNo == DesCardNo)
            {
                Destroy(ListHandCard[i]);

                //将删除的手牌从列表移除
                ListHandCard.Remove(ListHandCard[i]);

                //计算动画需要旋转的角度
                RotateAngel();

                //使用手牌时播放的动画
                UseCardAnimation();

                return;
            }
        }
    }

    //弃牌
    public void AbandonmentCard()
    {
        if (ListHandCard.Count > 5)
        {
            isAbandonment = true;
            print("还需弃 " + (ListHandCard.Count - 5) + " 张牌");
        }
        else
        {
            isAbandonment = false;
            roundManager.EndingStart();
        }
    }

    //测试用，随机生成一张游戏王手牌
    //public void YGOHandGeneration()
    //{
    //    int r = Random.Range(0, YGO_Card.Length);
    //    NextCard = YGO_Card[r];
    //}
}
