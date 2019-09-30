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

    //动画物体
    public List<GameObject> effect_A;
    public List<GameObject> effect_C;

    //动画播放位置
    public Transform enemyHeadPosition;
    public Transform myHeadPosition;

    //用于在攻击动画结束后播放反击动画
    public bool attackJustOver;
    private int effect_cCard;
    private bool effect_isMyAction;

    private void Start()
    {
        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        enemyPlayer = GameObject.Find("EnemyUI").GetComponent<PlayerManager>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();

        attackJustOver = false;
    }

    public void ActionEffect(AttackCard aCard, CounterCard cCard, bool isMyAction)
    {
        print(aCard.CardName + " " + cCard.CardName);

        int damage = aCard.Damage;//伤害
        int backDamage = 0; //反击伤害
        Attribute backAttribute = Attribute.Physical;  //反击伤害类型

        PlayerManager user; //进攻方
        PlayerManager opposite; //反击方
        Transform userHead; //进攻方动画播放位置
        Transform oppositeHead;//反击方动画播放位置

        float r_miss = 0; //闪避值，超过500闪避成功

        if (isMyAction)
        {
            user = myPlayer;
            opposite = enemyPlayer;
            userHead = myHeadPosition;
            oppositeHead = enemyHeadPosition;
        }
        else
        {
            user = enemyPlayer;
            opposite = myPlayer;
            userHead = enemyHeadPosition;
            oppositeHead = myHeadPosition;
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
                user.HpChange(3, 0);
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
                r_miss = Random.Range(0, 1000f);
                if (r_miss > 500)
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

        if (r_miss > 500)
        {
            //闪避
            ShowEffect(effect_C[2], userHead);
        }
        else
        {
            //没有闪避，播放进攻牌动画
            switch (aCard.attackCardNo)
            {
                case 1:
                    float r_color = Random.Range(0, 1000f);
                    if (r_color < 200) ShowEffect(effect_A[0], oppositeHead);
                    else if (r_color < 400) ShowEffect(effect_A[1], oppositeHead);
                    else if (r_color < 600) ShowEffect(effect_A[2], oppositeHead);
                    else if (r_color < 800) ShowEffect(effect_A[3], oppositeHead);
                    else ShowEffect(effect_A[4], oppositeHead);
                    break;
                case 2:
                    ShowEffect(effect_A[5], oppositeHead);
                    break;
                case 3:
                    ShowEffect(effect_A[6], oppositeHead);
                    break;
                case 4:
                    ShowEffect(effect_A[7], oppositeHead);
                    break;
                case 5:
                    ShowEffect(effect_A[9], userHead);
                    break;
                case 6:
                    ShowEffect(effect_A[10], oppositeHead);
                    break;
                case 7:
                    ShowEffect(effect_A[11], oppositeHead);
                    break;
                case 8:
                    ShowEffect(effect_A[12], oppositeHead);
                    break;
                case 9:
                    ShowEffect(effect_A[13], userHead);
                    break;
                case 10:
                    ShowEffect(effect_A[14], oppositeHead);
                    break;
            }
            attackJustOver = true;
            effect_cCard = cCard.counterCardNo;
            effect_isMyAction = isMyAction;
        }


        user.HpChange(-backDamage, 2);
        opposite.HpChange(-damage, 2);
    }

    //播放特效
    private void ShowEffect(GameObject effect, Transform site)
    {
        GameObject texiao = Instantiate(effect) as GameObject;
        texiao.GetComponent<UGUISpriteAnimation>().Loop = false;
        texiao.transform.position = site.position;
        texiao.transform.parent = site;
    }

    public void TakeBurn(bool isMyTurn)
    {
        if (isMyTurn)
        {
            ShowEffect(effect_A[8], myHeadPosition);
        }
        else
        {
            ShowEffect(effect_A[8], enemyHeadPosition);
        }
    }

    //在进攻动画播放后播放反击动画
    public void EffectCounter()
    {

        Transform userHead; //进攻方动画播放位置
        Transform oppositeHead;//反击方动画播放位置

        if (effect_isMyAction)
        {
            userHead = myHeadPosition;
            oppositeHead = enemyHeadPosition;
        }
        else
        {
            userHead = enemyHeadPosition;
            oppositeHead = myHeadPosition;
        }

        switch (effect_cCard)
        {
            case 1:
                ShowEffect(effect_C[0], oppositeHead);
                break;
            case 2:
                ShowEffect(effect_C[1], oppositeHead);
                break;
            case 4:
                ShowEffect(effect_C[3], userHead);
                break;
            case 5:
                ShowEffect(effect_C[4], userHead);
                break;
            case 6:
                ShowEffect(effect_C[5], oppositeHead);
                break;
            case 7:
                ShowEffect(effect_C[6], oppositeHead);
                break;
            case 8:
                ShowEffect(effect_C[7], oppositeHead);
                break;
        }
    }
}
