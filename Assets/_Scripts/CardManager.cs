using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //手牌实例：手牌点击功能实现，手牌属性

    public CounterCard counterCard;
    public AttackCard attackCard;

    public Image attackImage;
    public Image attackBallImage;
    public Image counterImage;
    public Image counterBallImage;
    public Image damageCubeImage;

    public Text attackPointText;
    public Text counterPointText;
    public Text damageText;

    //卡面介绍
    public GameObject IntroductionFrame;
    public Image IntroductionAI;
    public Image IntroductionCI;
    public Text IntroductionAT;
    public Text IntroductionCT;

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

        //介绍框获取
        IntroductionFrame = GameObject.Find("Introduction");
        IntroductionAI = GameObject.Find("AImage").GetComponent<Image>();
        IntroductionCI = GameObject.Find("CImage").GetComponent<Image>();
        IntroductionAT = GameObject.Find("AText").GetComponent<Text>();
        IntroductionCT = GameObject.Find("CText").GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IntroductionFrame.SetActive(true);
        IntroductionAI.sprite = attackCard.attackSprite;
        IntroductionCI.sprite = counterCard.counterSprite;
        IntroductionAT.text = "卡名：" + attackCard.CardName 
            + "\n效果：" + attackCard.CardEffect + "\n介绍：" + attackCard.CardIntroduction;
        IntroductionCT.text = "卡名：" + counterCard.CardName
            + "\n效果：" + counterCard.CardEffect + "\n介绍：" + counterCard.CardIntroduction;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IntroductionFrame.SetActive(false);
    }
}
