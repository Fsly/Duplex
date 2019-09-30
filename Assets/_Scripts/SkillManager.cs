using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //英雄技能和觉醒

    private RoundManager roundManager;//回合管理

    private PlayerManager myPlayer;//我方玩家类
    private PlayerManager enemyPlayer;//对方玩家类

    private CardCurved cardCurved;//我方手牌
    private EnemyHCurved enemyHCurved;//对方手牌

    private void Start()
    {
        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        enemyPlayer = GameObject.Find("EnemyUI").GetComponent<PlayerManager>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    public void SkillEffect(int heroNo, bool isMyAction)
    {
        PlayerManager user; //进攻方
        PlayerManager opposite; //反击方

        if (isMyAction)
        {
            user = myPlayer;
            opposite = enemyPlayer;
        }
        else
        {
            user = enemyPlayer;
            opposite = myPlayer;
        }

        //主要阶段发动挑战者技能
        if (heroNo == 1 && isMyAction)
        {
            enemyHCurved.RandomDestroyCard();
            user.HpChange(-1, 1);
        }
        else if (heroNo == 2 && !isMyAction)
        {
            cardCurved.RandomDestroyCard();
            user.HpChange(-1, 1);
        }

        //结束阶段发动光之猎手技能
        if (heroNo == 4 && isMyAction)
        {
            cardCurved.GetCards();
            cardCurved.AddCardAnimations();
        }
        else if (heroNo == 4 && !isMyAction)
        {
            enemyHCurved.HCNumChange(1);
        }
    }

    public void AwakeEffect(int awakeNo, bool isMyAction)
    {
        PlayerManager user; //进攻方
        PlayerManager opposite; //反击方

        if (isMyAction)
        {
            user = myPlayer;
            opposite = enemyPlayer;
        }
        else
        {
            user = enemyPlayer;
            opposite = myPlayer;
        }
    }

    //显示和技能有关的调用
    internal static void printSkill(string v)
    {
        print("技能信息： " + v);
    }

    //显示和觉醒有关的调用
    internal static void printAwake(string v)
    {
        print("觉醒信息： " + v);
    }
}
