using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //玩家类
    //用于双方头像框UI
    //实现HP和AP动画（暂未）

    public PlayerType type;//玩家类型

    public Hero hero;//选用的英雄
    public Awake awake;//选用的觉醒

    public int HP; //当前HP
    public int AP; //当前AP

    //UI
    public Text HPText;
    public Slider HPSlider;
    public GameObject APBar;
    public Image heroHeadImage;

    public List<GameObject> ListAPBall = new List<GameObject>(); //AP显示列表 

    //AP Prefab
    public GameObject APBall;

    private RoundManager roundManager;
    private EnemyHCurved enemyHCurved;
    private CardCurved cardCurved;
    public PlayerManager enemyPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ApChange(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ApChange(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HpChange(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HpChange(1);
        }
    }

    //初始化
    public void Init()
    {
        //初始化HP
        HP = hero.MaxHP;
        HPText.text = HP + "/" + hero.MaxHP;
        HPSlider.value = 1f;
        heroHeadImage.sprite = hero.headSprite;

        //初始化AP
        AP = 3;
        for (int i = 0; i < 3; i++)
        {
            GameObject GOAPBall = Instantiate(APBall) as GameObject;
            GOAPBall.transform.parent = APBar.transform;
            ListAPBall.Add(GOAPBall);
        }

        //物体获取
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
    }

    //生命变化（数值改变，显示动画）
    public void HpChange(int addNum)
    {
        HP += addNum;
        HPText.text = HP + "/" + hero.MaxHP;
        if (HP >= 0) HPSlider.value = (float)HP / hero.MaxHP;
    }

    //行动点变化（数值改变，显示动画）
    public void ApChange(int addNum)
    {
        if (-addNum > AP) addNum = -AP;
        AP += addNum;
        if (addNum > 0)
        {
            for (int i = 0; i < addNum; i++)
            {
                GameObject GOAPBall = Instantiate(APBall) as GameObject;
                GOAPBall.transform.parent = APBar.transform;
                ListAPBall.Add(GOAPBall);
            }
        }
        else if(addNum < 0)
        {
            for (int i = 0; i < -addNum; i++)
            {
                Destroy(ListAPBall[0]);
                ListAPBall.Remove(ListAPBall[0]);
            }
        }
    }

    //AP是否够
    public bool IsApEnough(int needAp)
    {
        if (needAp > AP) return false;
        else return true;
    }

    //回复体力
    public void ApGetToStart()
    {
        //如果是第一回合，双方抽牌，恢复体力
        if (roundManager.roundNum == 1)
        {
            ApChange(-1);
        }
        else
        {
            if (AP == 0) ApChange(3);
            else if(AP == 1) ApChange(2);
            else if (AP == 2) ApChange(1);
        }
    }
}

//卡种记号
public enum PlayerType
{
    player1,
    player2
}

