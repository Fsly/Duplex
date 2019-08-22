using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCardManager : MonoBehaviour
{
    //对方手牌实例,对方出牌实例
    //对方手牌,出牌属性
    //没有点击，因为看不见所以不显示:)

    //卡牌信息存储
    public CounterCard counterCard;
    public AttackCard attackCard;

    //手牌功能
    public int handCardNo;//手牌号

    //出牌类
    public EnemyShowCard showingCard;

    //卡面位置
    public Transform t_CardBack;

    // Start is called before the first frame update
    void Start()
    {
        showingCard = GameObject.Find("EnemyShowCardParent").GetComponent<EnemyShowCard>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterAnimation()
    {
        t_CardBack.DOLocalMoveY(-20 , 0.2f);
    }

    public void UseCard()
    {
        //开启动画协程
        StartCoroutine(InstantiateShowCard());

        //删除卡牌
        GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>().DestroyTheCard(handCardNo);
    }

    IEnumerator InstantiateShowCard()
    {
        //出牌动画协程

        //赋值
        showingCard.attackCard = new AttackCard(attackCard);
        showingCard.counterCard = new CounterCard(counterCard);

        //出牌
        showingCard.InstantiateInit();
        yield return null;
    }
}
