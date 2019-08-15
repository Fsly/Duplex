using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySay : MonoBehaviour
{
    //获取组件
    private RoundManager roundManager;
    private HeroAwakeCurved heroAwakeCurved;

    //敌方选择
    public Hero hero;
    public Awake awake;

    // Start is called before the first frame update
    void Start()
    {
        //物体获取
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        heroAwakeCurved = GameObject.Find("HeroAwakePrefab").GetComponent<HeroAwakeCurved>();
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
    }
}
