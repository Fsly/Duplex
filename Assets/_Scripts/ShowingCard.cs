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

    //生成牌
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


        if (roundManager.isMyturn && roundManager.roundPhase == RoundPhase.Main && roundManager.waitCounter == WaitPhase.NoWait)
        {
            //打出进攻卡
            if (attackCard.canCounter && enemy.AP != 0)
            {
                //如果可以反击，等待对方反击
                WaitForCounter();
            }
        }
        else
        {
            //打出反击卡
            print("我方反击");
            roundManager.MeWaitOK();

            GameObject.Find("NothingButton(Clone)").GetComponent<NotCounter>().BeDestroy();
        }
    }

    //等待对方反击
    public void WaitForCounter()
    {
        roundManager.WaitingEnemy();
    }

}
