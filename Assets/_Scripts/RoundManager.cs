﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    //回合管理
    //回合周期
    //背景

    public GameObject StartUI;//开始的UI
    public GameObject getCardPrefabs;//抽牌预制体
    public Transform MainCanvas;//主画布

    //背景管理
    public Sprite[] BgSprite;
    public Image BgImage;
    public int BgNo;

    public int roundNum; //回合计数
    public bool isMyturn;//谁的回合
    public RoundPhase roundPhase; //当前状态

    public PlayerManager myPlayer;//我方玩家类

    private CardCurved cardCurved;

    // Start is called before the first frame update
    void Start()
    {
        //随机场景
        RandomBackGround();
    }

    public void GameStartReady()
    {
        //初始化赋值
        RoundGoingStart();

        //目前设定我方先开，所有进入我方准备阶段
        PreparatoryRoundStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (BgNo < BgSprite.Length - 1) BgNo += 1;
            else BgNo = 0;
            BgImage.sprite = BgSprite[BgNo];
        }
    }

    //随机背景
    void RandomBackGround()
    {
        BgNo = Random.Range(0, BgSprite.Length);
        BgImage.sprite = BgSprite[BgNo];
    }

    //游戏初始化赋值
    public void RoundGoingStart()
    {
        roundPhase = RoundPhase.Preparatory;
        roundNum = 1;
        isMyturn = true;

        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
    }

    //准备阶段初始化
    public void PreparatoryRoundStart()
    {
        roundPhase = RoundPhase.Preparatory;

        //回合开始动画
        Instantiate(StartUI, MainCanvas);

        //回复行动点
        myPlayer.ApGetToStart();
    }

    //抽牌阶段初始化
    public void DrawRoundStart()
    {
        roundPhase = RoundPhase.Draw;

        Instantiate(getCardPrefabs, MainCanvas);
    }

    //主要阶段初始化
    public void MainRoundStart()
    {
        roundPhase = RoundPhase.Main;
    }

    //弃牌阶段初始化
    public void AbandonmentRoundStart()
    {
        roundPhase = RoundPhase.Abandonment;

        cardCurved.AbandonmentCard();
    }
}

//阶段:准备,抽牌,主要,弃牌,结束
public enum RoundPhase
{
    Preparatory,
    Draw,
    Main,
    Abandonment,
    Ending
}

