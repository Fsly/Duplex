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

    private RoundManager roundManager;

    private EnemyHCurved enemyHCurved;

    // Start is called before the first frame update
    void Start()
    {
        showingCard = GameObject.Find("EnemyShowCardParent").GetComponent<EnemyShowCard>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterAnimation()
    {
        t_CardBack.DOLocalMoveY(-20 , 0.2f);
    }

    //使用卡，通过外界调用此函数实现打出
    public void UseCard()
    {
        if (roundManager.waitCounter != WaitPhase.WaitMe &&
            !showingCard.delayAttack) 
        {
            //开启动画协程
            StartCoroutine(InstantiateShowCard());

            //删除卡牌
            enemyHCurved.DestroyTheCard(handCardNo);
        }
    }

    //弃牌
    public void DestroyCard()
    {
        if (roundManager.roundPhase == RoundPhase.Abandonment && !roundManager.isMyturn)
        {
            //处于我方弃牌阶段，保留5张，判断是否还需弃牌

            enemyHCurved.DestroyTheCard(handCardNo);
            enemyHCurved.AbandonmentCard();
        }
        else if (showingCard.delayAttack)
        {
            //流星雨弃牌中，最大弃3张，每张伤害+1

            if (showingCard.addDamage < 3)
            {
                enemyHCurved.DestroyTheCard(handCardNo);
                showingCard.addDamage++;
            }
        }
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
