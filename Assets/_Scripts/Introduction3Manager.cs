using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Introduction3Manager : MonoBehaviour
{
    public Text IntroductionHT3;
    public Text IntroductionAT3;

    public Vector3 IntroductionInit;

    public Hero hero;
    public Awake awake;

    // Start is called before the first frame update
    void Start()
    {

        IntroductionInit = GameObject.Find("IntroductionInit").GetComponent<Transform>().position;
        IntroductionHT3 = GameObject.Find("HText3").GetComponent<Text>();
        IntroductionAT3 = GameObject.Find("AText3").GetComponent<Text>();
    }

    public void HiddenIntroduction()
    {
        //隐藏介绍框
        transform.DOMove(IntroductionInit, 0.5f);
    }

    public void DisplayIntroduction()
    {
        //显示介绍框
        transform.DOMove(IntroductionInit + new Vector3(-420, 0, 0), 0.5f);
    }

    public void UpdateForDisplay()
    {
        //更新内容
        IntroductionHT3.text = "英雄：" + hero.monicker + "\n效果：" + hero.effect;
        IntroductionAT3.text = "觉醒：" + awake.monicker + "\n效果：" + awake.effect;
    }

    public void UpdateForDisplay2()
    {
        //更新内容，隐藏觉醒
        IntroductionHT3.text = "英雄：" + hero.monicker + "\n效果：" + hero.effect;
        IntroductionAT3.text = "觉醒：？？？\n效果：？？？";
    }
}
