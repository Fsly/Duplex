using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardCurved : MonoBehaviour
{
    //实现手牌区域的曲面化以及DOTween实现旋转
    //手牌储存，抽牌

    public GameObject[] YGO_Card; //代替用牌

    public AttackCard[] attackCards; //进攻牌
    public CounterCard[] counterCards; //反击牌

    public GameObject NextCard; //下一张手牌

    public List<GameObject> ListHandCard = new List<GameObject>(); //手牌列表 
    public GameObject TransBeginHandCard;  //生成手牌最开始的位置
    public float _FloRotateAngel; //手牌动画旋转的角度

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //响应按键，待改
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //抽牌

            //YGO测试
            //YGOHandGeneration();

            RandomGeneration();
            GetCards();
            AddCardAnimations();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //出最后那张牌
            UseCardAnimation();
        }
    }



    public void GetCards()
    {
        //克隆下一张手牌
        GameObject GOHandCard = Instantiate(NextCard) as GameObject;
        GOHandCard.transform.position = TransBeginHandCard.transform.position;
        GOHandCard.transform.parent = transform;

        //将新手牌添加到手牌列表
        ListHandCard.Add(GOHandCard);

        //计算动画需要旋转的角度
        RotateAngel();

    }

    //为手牌添加动画
    private void HandCardAnimation(GameObject GO, float Vec3_Z)
    {
        GO.transform.DORotate(new Vector3(0, 0, Vec3_Z), 0.3F, RotateMode.Fast);
    }

    //增加手牌时播放的动画
    private void AddCardAnimations()
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
    private void UseCardAnimation()
    {
        Destroy(ListHandCard[0]);
        ListHandCard.Remove(ListHandCard[0]);
        RotateAngel();
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

    //计算需要旋转的角度
    private void RotateAngel()
    {
        _FloRotateAngel = 55F / ListHandCard.Count / ListHandCard.Count;
    }

    //public void YGOHandGeneration()
    //{
    //    //测试用，随机生成一张游戏王手牌
    //    int r = Random.Range(0, YGO_Card.Length);
    //    //Debug.Log(r);
    //    NextCard = YGO_Card[r];
    //}

    //随机生成一张牌
    public void RandomGeneration()
    {
        int r1= Random.Range(0, attackCards.Length);
        int r2 = Random.Range(0, counterCards.Length);
        NextCard.GetComponent<CardManager>().attackCard = new AttackCard(attackCards[r1]);
        NextCard.GetComponent<CardManager>().counterCard = new CounterCard(counterCards[r2]);
    }

}
