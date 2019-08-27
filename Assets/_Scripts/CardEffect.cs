using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    private RoundManager roundManager;//回合管理

    private PlayerManager myPlayer;//我方玩家类
    private PlayerManager enemyPlayer;//对方玩家类

    private CardCurved cardCurved;//我方手牌
    private EnemyHCurved enemyHCurved;//对方手牌

    private AttackCard attackCard;//进攻牌
    private CounterCard counterCard;//反击牌

    private void Start()
    {
        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        enemyPlayer = GameObject.Find("EnemyUI").GetComponent<PlayerManager>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    public void ActionEffect(AttackCard aCard, CounterCard cCard,bool isMyAction)
    {
        int damage = aCard.Damage;
        int backDamage = 0;
        Attribute backAttribute = Attribute.Physical;

        PlayerManager user;
        PlayerManager opposite;

        if (isMyAction)
        {
            user = myPlayer;
            opposite = enemyPlayer;
        }
        else
        {
            user = enemyPlayer;
            opposite= myPlayer;
        }

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

                break;

        }

    }
}
