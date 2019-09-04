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

    //AP，HP计算，没计算反击，故废弃
    public void HpApChange()
    {
        //我方AP减少，敌方HP减少
        user.ApChange(-attackCard.ActionPoint);
        enemy.HpChange(-attackCard.Damage);
    }

    //生成牌，出牌动画，先行效果
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
        user.ApChange(-attackCard.ActionPoint);

        if (roundManager.isMyturn && roundManager.roundPhase == RoundPhase.Main && roundManager.waitCounter == WaitPhase.NoWait)
        {
            //打出进攻卡


            //进攻牌先行效果
            switch (attackCard.attackCardNo)
            {
                case 2:
                    //上挑
                    enemyHCurved.RandomDestroyCard();
                    break;
                case 10:
                    //流星雨
                    delayAttack = true;
                    enemyShowCard.enemyAttack = attackCard;
                    //生成发射按钮
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
            print("我方反击");
            roundManager.MeWaitOK();

            //判定
            cardEffect.ActionEffect(enemyAttack, counterCard, false);

            GameObject.Find("NothingButton(Clone)").GetComponent<NotCounter>().BeDestroy();
        }
    }

    //进攻牌动作
    public void AttackAction()
    {
        if (attackCard.canCounter)
        {
            //如果可以反击，传值并等待对方反击
            if (!user.darkfire)
            {
                enemyShowCard.enemyAttack = attackCard;
                WaitForCounter();
            }
        }
        else
        {
            //不可反击直接判定
            cardEffect.ActionEffect(attackCard, noCounterCard, true);
        }
    }

    //等待对方反击
    public void WaitForCounter()
    {
        roundManager.WaitingEnemy();
    }

}
