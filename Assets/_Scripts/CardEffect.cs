using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    private RoundManager roundManager;//回合管理

    private PlayerManager myPlayer;//我方玩家类
    private PlayerManager enemyPlayer;//对方玩家类x

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

    public void ActionEffect(AttackCard aCard, CounterCard cCard, bool isMyAction)
    {
        print(aCard.CardName + " " + cCard.CardName);

        int damage = aCard.Damage;//伤害
        int backDamage = 0; //反击伤害
        Attribute backAttribute = Attribute.Physical;  //反击伤害类型

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


        //反击牌效果
        switch (cCard.counterCardNo)
        {
            case 5:
                //暗器
                backDamage = 1;
                break;
            case 6:
                //幸运牌
                if (isMyAction)
                {
                    enemyHCurved.HCNumChange(2);
                }
                else
                {
                    cardCurved.GetCards();
                    cardCurved.GetCards();
                    cardCurved.AddCardAnimations();
                }
                break;
            case 7:
                //停战号令
                roundManager.AbandonmentRoundStart();
                break;
            case 8:
                //气流吸纳
                opposite.saveAp++;
                break;
        }

        //进攻牌效果
        switch (aCard.attackCardNo)
        {

            case 4:
                //魔力引爆
                opposite.burnDamage += 2;
                break;
            case 5:
                //恢复魔法
                user.HpChange(3);
                break;
            case 7:
                //正义审判
                if (user.HP > opposite.HP)
                {
                    damage++;
                }
                break;
            case 8:
                //结晶风暴
                opposite.saveAp--;
                break;
            case 9:
                //黑炎仪式
                user.darkfire = true;
                break;
        }

        //黑炎Buff生效
        if (user.darkfire)
        {
            damage *= 2;
            if (aCard.attackCardNo != 9) user.darkfire = false;
        }

        //反击牌改变伤害效果
        switch (cCard.counterCardNo)
        {
            case 1:
                //钢盾
                if (aCard.damageAttribute == Attribute.Physical)
                {

                    damage = 1;
                }
                break;
            case 2:
                //图纹盾
                damage -= 1;
                break;
            case 3:
                //闪避
                float r = Random.Range(0, 1000f);
                if (r < 500)
                {
                    damage = 0;
                }
                break;
            case 4:
                //魔法反射
                if (aCard.damageAttribute == Attribute.Magic)
                {
                    backDamage = damage;
                    backAttribute = Attribute.Magic;
                    damage = 0;
                }
                break;
        }

        user.HpChange(-backDamage);
        opposite.HpChange(-damage);
    }
}
