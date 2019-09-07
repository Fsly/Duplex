using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyShowCard : MonoBehaviour
{
    //对方出牌管理类
    //对方出牌实例：生成出牌，出牌动画
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

    private ShowingCard showingCard;

    private CardCurved cardCurved;//对方手牌

    public bool delayAttack;//延时判定流星雨

    public int addDamage;//附加伤害

    private void Start()
    {
        cardEffect = GameObject.Find("AllCardEffect").GetComponent<CardEffect>();
        showingCard= GameObject.Find("ShowCardParent").GetComponent<ShowingCard>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();

        delayAttack = false;
        addDamage = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InstantiateInit();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //对方流星雨
            attackCard.Damage += addDamage;
            addDamage = 0;
            showCardManager.init();
            AttackAction();
            delayAttack = false;
        }
    }

    //出牌动画
    public void CardShowAnimation(Transform cardTransform)
    {
        Vector3 bahandSet = new Vector3(0, 100f, 0);
        cardTransform.parent = transform;
        cardTransform.position = transform.position + bahandSet;
        cardTransform.DOMove(transform.position, 0.5f).SetEase(Ease.OutBack);
    }

    //生成牌，播放出牌动画，消耗行动点，先行效果
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
            user.ApChange(-attackCard.ActionPoint);
        }
        else
        {
            user.ApChange(-counterCard.ActionPoint);
        }

        if (roundManager.isMyturn && 
            roundManager.waitCounter == WaitPhase.WaitEnemy)
        {
            //打出反击卡

            //等待结束
            roundManager.EnemyWaitOK();
            roundManager.WaitPrefabOff();

            //判定
            cardEffect.ActionEffect(enemyAttack, counterCard, true);
        }
        else
        {
            //打出进攻卡

            //进攻牌先行效果
            switch (attackCard.attackCardNo)
            {
                case 2:
                    //上挑
                    cardCurved.RandomDestroyCard();
                    break;
                case 10:
                    //流星雨
                    delayAttack = true;
                    showingCard.enemyAttack = attackCard;
                    //额外伤害归零
                    addDamage = 0;
                    break;
            }
            if (!delayAttack)
            {
                AttackAction();
            }
        }
    }

    //进攻牌动作
    public void AttackAction()
    {
        if (attackCard.canCounter && !user.darkfire)
        {
            // 如果可以反击并且没有黑炎buff，传值并等待对方反击
            roundManager.WaitingMe();
            roundManager.WaitPrefabOff();
            showingCard.enemyAttack = attackCard;

            // 不出牌按钮传值
            GameObject.Find("NothingButton(Clone)").GetComponent<NotCounter>().enemyAttack = attackCard;
        }
        else
        {
            // 不可反击直接判定
            cardEffect.ActionEffect(attackCard, noCounterCard, false);
        }
    }

}
