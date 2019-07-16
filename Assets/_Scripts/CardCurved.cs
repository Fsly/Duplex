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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            HandGeneration();
            GetCards();
            AddCardAnimations();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            UseCardAnimation();
        }
    }

    public void HandGeneration()
    {
        //随机生成一张手牌
        int r = Random.Range(0, YGO_Card.Length);
        //Debug.Log(r);
        NextCard = YGO_Card[r];
    }

    public void GetCards()
    {
        //克隆预设
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

}
