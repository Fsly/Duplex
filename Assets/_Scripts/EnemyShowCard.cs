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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InstantiateInit();
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

    //AP，HP计算,没计算反击，故废弃
    public void HpApChange()
    {
        //我方HP减少，敌方AP减少
        user.ApChange(-attackCard.ActionPoint);
        enemy.HpChange(-attackCard.Damage);
    }

    //生成牌，播放出牌动画
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

        if (roundManager.isMyturn && roundManager.waitCounter == WaitPhase.WaitEnemy)
        {
            //反击
            roundManager.EnemyWaitOK();
        }
        else
        {
            //进攻
            if (attackCard.canCounter && enemy.AP != 0)
            {
                roundManager.WaitingMe();
            }
        }
    }

}
