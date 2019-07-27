using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowingCard : MonoBehaviour
{
    //我方出牌实例：出牌动画，出牌属性及显示

    //生成的显示卡（打出卡）
    public GameObject showCard;
    public CardManager showCardManager;

    //卡牌信息存储
    public CounterCard counterCard;
    public AttackCard attackCard;

    //出牌动画
    public void CardShowAnimation(Transform cardTransform)
    {
        Vector3 bahandSet = new Vector3(0, -100f, 0);
        cardTransform.parent = transform;
        cardTransform.position = transform.position + bahandSet;
        cardTransform.DOMove(transform.position, 0.5f).SetEase(Ease.OutBack);
    }

    //生成牌
    public void InstantiateInit()
    {
        GameObject GoShowCard = Instantiate(showCard) as GameObject;
        showCardManager = GoShowCard.GetComponent<CardManager>();
        showCardManager.attackCard = attackCard;
        showCardManager.counterCard = counterCard;
        showCardManager.cardButton = HandCardButton.Cannot;
        showCardManager.init();

        //出牌动画
        CardShowAnimation(GoShowCard.transform);
    }

}
