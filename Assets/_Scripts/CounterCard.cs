using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterCard : MonoBehaviour
{
    //反击卡
    public int counterCardNo; //反击卡号
    public string CardName;//卡名
    public string CardIntroduction;//卡介绍
    public string CardEffect;//卡效果

    //逻辑层面
    public Attribute counterAttribute;//卡种类为物理or魔法
    public int ActionPoint;//行动点

    //UI层面
    public Sprite counterSprite;  //图片
    public Sprite counterBallSprite; //行动点球

    public CounterCard(CounterCard card)
    {
        counterCardNo = card.counterCardNo;
        CardName = card.CardName;
        CardIntroduction = card.CardIntroduction;
        CardEffect = card.CardEffect;
        counterAttribute = card.counterAttribute;
        ActionPoint = card.ActionPoint;
        counterSprite = card.counterSprite;
        counterBallSprite = card.counterBallSprite;
    }
}

