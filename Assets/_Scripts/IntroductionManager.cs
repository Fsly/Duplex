using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroductionManager : MonoBehaviour
{
    //介绍框综合管理

    //公共介绍框UI
    public Transform IntroductionFrame;
    public Image IntroductionAI;
    public Image IntroductionCI;
    public Text IntroductionAT;
    public Text IntroductionCT;
    public Vector3 IntroductionInit;

    //卡牌信息
    public int handCardNo;//手牌号
    public CounterCard counterCard;
    public AttackCard attackCard;


    // Start is called before the first frame update
    void Start()
    {
        //介绍框获取
        IntroductionFrame = GameObject.Find("Introduction").GetComponent<Transform>();
        IntroductionInit = GameObject.Find("IntroductionInit").GetComponent<Transform>().position;
        IntroductionAI = GameObject.Find("AImage").GetComponent<Image>();
        IntroductionCI = GameObject.Find("CImage").GetComponent<Image>();
        IntroductionAT = GameObject.Find("AText").GetComponent<Text>();
        IntroductionCT = GameObject.Find("CText").GetComponent<Text>();

        //初始手牌号
        handCardNo = -1;
    }

    public void HiddenIntroduction()
    {
        //隐藏介绍框
        IntroductionFrame.DOMove(IntroductionInit, 0.5f);
    }

    public void DisplayIntroduction()
    {
        //显示介绍框
        IntroductionFrame.DOMove(IntroductionInit + new Vector3(-420, 0, 0), 0.5f);
    }

    public void UpdateForDisplay()
    {
        //更新内容，为了显示
        IntroductionAI.sprite = attackCard.attackSprite;
        IntroductionCI.sprite = counterCard.counterSprite;
        IntroductionAT.text = "卡名：" + attackCard.CardName + " " + attackCard.ActionPoint + "/" + attackCard.Damage
            + "\n效果：" + attackCard.CardEffect + "\n介绍：" + attackCard.CardIntroduction;
        IntroductionCT.text = "卡名：" + counterCard.CardName + " " + counterCard.ActionPoint
            + "\n效果：" + counterCard.CardEffect + "\n介绍：" + counterCard.CardIntroduction;
    }

}
