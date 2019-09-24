using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowingCard : MonoBehaviour
{
    //我方出牌管理类
    //我方出牌实例：生成出牌，出牌动画
    //伤害计算

    //生成的显示卡（打出卡）
    public GameObject showCard;
    public CardManager showCardManager;

    public PlayerManager user;
    public PlayerManager enemy;

    //卡牌信息存储
    public CounterCard counterCard;
    public AttackCard attackCard;

    public GameObject GoShowCard;

    public RoundManager roundManager;

    public CounterCard noCounterCard;//不打出反击牌

    public AttackCard enemyAttack;

    private CardEffect cardEffect;

    private EnemyShowCard enemyShowCard;

    private CardCurved cardCurved;//我方手牌

    private EnemyHCurved enemyHCurved;//对方手牌

    public bool delayAttack;//延时判定流星雨

    public GameObject FireballGO;

    public int addDamage;//附加伤害

    private void Start()
    {
        cardEffect = GameObject.Find("AllCardEffect").GetComponent<CardEffect>();
        enemyShowCard= GameObject.Find("EnemyShowCardParent").GetComponent<EnemyShowCard>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();

        delayAttack = false;
        addDamage = 0;
    }

    //出牌动画
    public void CardShowAnimation(Transform cardTransform)
    {
        Vector3 bahandSet = new Vector3(0, -100f, 0);
        cardTransform.parent = transform;
        cardTransform.position = transform.position + bahandSet;
        cardTransform.DOMove(transform.position, 0.5f).SetEase(Ease.OutBack);
    }

    //生成牌，出牌动画，消耗行动点，先行效果
    public void InstantiateInit()
    {
        Destroy(GoShowCard);
        GoShowCard = Instantiate(showCard) as GameObject;
        showCardManager = GoShowCard.GetComponent<CardManager>();
        showCardManager.attackCard = attackCard;
        showCardManager.counterCard = counterCard;
        showCardManager.cardButton = HandCardButton.Cannot;
        showCardManager.init();

        //出牌动画
        CardShowAnimation(GoShowCard.transform);

        //消耗行动点
        if (roundManager.isMyturn)
        {
            user.ApChange(-showCardManager.attackCard.ActionPoint);
        }
        else
        {
            user.ApChange(-showCardManager.counterCard.ActionPoint);
        }

        if (roundManager.isMyturn && 
            roundManager.roundPhase == RoundPhase.Main && 
            roundManager.waitCounter == WaitPhase.NoWait)
        {
            //打出进攻卡

            //水晶剑士技能
            if (user.hero.No == 2)
            {
                if (showCardManager.attackCard.damageAttribute == Attribute.Magic)
                {
                    SkillManager.printSkill("我方水晶剑士增加计数器");
                    user.HeroTimer++;
                }
                else if(showCardManager.attackCard.damageAttribute == Attribute.Physical)
                {
                    if (user.HeroTimer >= 1)
                    {
                        SkillManager.printSkill("我方水晶剑士使用技能");
                        user.HeroTimer = 0;
                        showCardManager.attackCard.Damage++;
                        showCardManager.init();
                    }
                }
            }

            //进攻牌先行效果
            switch (showCardManager.attackCard.attackCardNo)
            {
                case 2:
                    //上挑
                    enemyHCurved.RandomDestroyCard();
                    break;
                case 10:
                    //流星雨
                    delayAttack = true;
                    enemyShowCard.enemyAttack = showCardManager.attackCard;
                    //生成发射按钮（按钮计算额外伤害）
                    GameObject GOCardButton= Instantiate(FireballGO) as GameObject;
                    GOCardButton.transform.parent = GameObject.Find("MiddleButtonInit").transform;
                    GOCardButton.transform.position = GameObject.Find("MiddleButtonInit").transform.position;
                    break;
            }
            if (!delayAttack)
            {
                AttackAction();
            }
        }
        else
        {
            //打出反击卡

            //共鸣法师技能
            if (user.hero.No == 3)
            {
                if (enemyAttack.damageAttribute == Attribute.Magic)
                {
                    SkillManager.printSkill("我方共鸣法师使用技能");
                    user.HP++;
                }
            }

            //结束我方等待，等待对方回合
            roundManager.MeWaitOK();
            roundManager.WaitPrefabOn();

            //判定
            cardEffect.ActionEffect(enemyAttack, showCardManager.counterCard, false);

            //删除“不打出”按钮
            GameObject.Find("NothingButton(Clone)").GetComponent<NotCounter>().BeDestroy();
        }
    }

    //进攻牌动作
    public void AttackAction()
    {
        if (showCardManager.attackCard.canCounter && !user.darkfire)
        {
            //如果可以反击并且没有黑炎buff，传值并等待对方反击
            enemyShowCard.enemyAttack = showCardManager.attackCard;
            WaitForCounter();
        }
        else
        {
            if (user.darkfire)
            {
                showCardManager.damageText.color = Color.red;
            }

            //不可反击直接判定
            cardEffect.ActionEffect(showCardManager.attackCard, noCounterCard, true);
        }
    }

    //等待对方反击
    public void WaitForCounter()
    {
        roundManager.WaitingEnemy();
        roundManager.WaitPrefabOn();
    }

}
