using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySay : MonoBehaviour
{
    //获取组件
    private RoundManager roundManager;
    private HeroAwakeCurved heroAwakeCurved;
    private EnemyHCurved enemyHCurved;

    //敌方选择
    public Hero hero;
    public Awake awake;

    //敌方手牌
    public AttackCard m_attackCard;
    public CounterCard m_counterCard;

    // Start is called before the first frame update
    void Start()
    {
        //物体获取
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        heroAwakeCurved = GameObject.Find("HeroAwakePrefab").GetComponent<HeroAwakeCurved>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //对方准备好
            heroAwakeCurved.EnemyReady(hero, awake);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //我方先开
            heroAwakeCurved.decideWhoFrist = true;
            heroAwakeCurved.youFirst = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //对方先开
            heroAwakeCurved.decideWhoFrist = true;
            heroAwakeCurved.youFirst = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //对方手牌全部赋值为指定牌
            for (int i = 0; i < enemyHCurved.ListHandCard.Count; i++)
            {
                enemyHCurved.ListHandCard[i].GetComponent<EnemyCardManager>().attackCard = new AttackCard(m_attackCard);
                enemyHCurved.ListHandCard[i].GetComponent<EnemyCardManager>().counterCard = new CounterCard(m_counterCard);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //对方打出第一张手牌
            if ((roundManager.waitCounter == WaitPhase.WaitEnemy) || 
                (roundManager.roundPhase == RoundPhase.Main && 
                !roundManager.isMyturn && 
                roundManager.waitCounter != WaitPhase.WaitMe))
            {
                enemyHCurved.ListHandCard[0].GetComponent<EnemyCardManager>().UseCard();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //对方准备阶段抽牌
            if (roundManager.roundPhase == RoundPhase.Draw && !roundManager.isMyturn)
            {
                enemyHCurved.HCNumChange(2);
                roundManager.MainRoundStart();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //对方进入弃牌阶段
            if (roundManager.roundPhase == RoundPhase.Main && !roundManager.isMyturn)
            {
                roundManager.AbandonmentRoundStart();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            //对方弃第一张手牌
            enemyHCurved.ListHandCard[0].GetComponent<EnemyCardManager>().DestroyCard();
        }
    }
}
