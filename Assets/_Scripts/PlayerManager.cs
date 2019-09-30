using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    // 玩家类
    // 用于双方头像框UI
    // 实现HP和AP缓动动画

    public PlayerType type; //玩家类型

    public Hero hero; //选用的英雄
    public Awake awake;//选用的觉醒

    public int HP; //当前HP
    public int AP; //当前AP

    public int HeroTimer;//英雄技能计数器
    public int AwakeTimer;//觉醒技能计数器

    public bool awakeIsOpen;//觉醒已使用
    public bool awakeCanOpen;//觉醒条件已满足

    //UI
    public Text HPText;
    public Slider HPSlider;
    public GameObject APBar;
    public Image heroHeadImage;

    public Slider UpBar;
    public Slider DownBar;

    public List<GameObject> ListAPBall = new List<GameObject>(); //AP显示列表 

    //AP Prefab
    public GameObject APBall;

    private RoundManager roundManager;
    private EnemyHCurved enemyHCurved;
    private CardCurved cardCurved;
    private Transform I_Hyp;
    private CardEffect cardEffect;

    public PlayerManager enemyPlayer;

    //额外行动点
    public int saveAp;

    //燃烧伤害
    public int burnDamage;

    //黑炎仪式buff
    public bool darkfire;

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
            HpChange(-1,2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HpChange(1,2);
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
        I_Hyp = GameObject.Find("Hypovolemia").transform;
        cardEffect = GameObject.Find("AllCardEffect").GetComponent<CardEffect>();

        //初始值
        saveAp = 0;
        burnDamage = 0;
        darkfire = false;
        awakeIsOpen = false;
        HeroTimer = 0;
        AwakeTimer = 0;
    }

    //生命变化（数值改变，显示动画）
    //伤害来源：0.无来源，1.自己，2.对方
    public void HpChange(int addNum, int sourceOfDamage = 0)
    {
        //对方光之猎手技能
        if (enemyPlayer.hero.No == 4)
        {
            //对对方造成伤害
            if (sourceOfDamage == 2 && addNum < 0)
            {
                //在该玩家方回合
                if (roundManager.isMyturn != (type == PlayerType.player1))
                {
                    SkillManager.printSkill("光之猎手增加计数器");
                    enemyPlayer.HeroTimer++;
                }
            }
        }

        //死神状态改变
        if (awake.No == 1)
        {

        }

        //数值改变
        HP += addNum;

        //示数
        HPText.text = HP + "/" + hero.MaxHP;

        float ratio = (float)HP / hero.MaxHP;    //血量比例

        //血条改变
        if (addNum < 0)  // HP降低
        {
            UpBar.transform.localScale = Vector3.zero;             // 隐藏加血层
            HPSlider.DOValue(ratio, 0.1f);                         // 设置当前血量
            DownBar.DOValue(ratio, 1.5f);                          // 扣血层缓动缩放到当前血量
        }
        else if (addNum > 0)   // HP增加
        {
            UpBar.value = ratio;                                   // 设置加血层缩放
            UpBar.transform.localScale = Vector3.one;              // 显示加血层
            HPSlider.DOValue(ratio, 1.5f);                         // 播放加血动画到当前血量
        }

        //我方血量低效果
        if (type == PlayerType.player1 && HP < 4)
        {
            I_Hyp.localScale = Vector3.one;
        }
        else if (type == PlayerType.player1 && HP > 3)
        {
            I_Hyp.localScale = Vector3.zero;
        }
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
                if (type == PlayerType.player2)
                {
                    GOAPBall.GetComponent<PointMask>().OnPointAdd();
                }
                else
                {
                    ListAPBall[0].GetComponent<PointMask>().OnPointAdd();
                }
            }
        }
        else if (addNum < 0)
        {
            for (int i = 0; i < -addNum; i++)
            {
                //Destroy(ListAPBall[0]);

                if (type == PlayerType.player2)
                {
                    ListAPBall[ListAPBall.Count - 1].GetComponent<PointMask>().OnPointExit();

                    ListAPBall.Remove(ListAPBall[ListAPBall.Count - 1]);
                }
                else
                {
                    ListAPBall[0].GetComponent<PointMask>().OnPointExit();

                    ListAPBall.Remove(ListAPBall[0]);
                }
            }
        }
    }

    //AP是否够
    public bool IsApEnough(int needAp)
    {
        if (needAp > AP) return false;
        else return true;
    }

    //准备阶段回复体力
    public void ApGetToStart()
    {
        
        if (roundManager.roundNum == 1)
        {
            //如果是第一回合，2体力
            ApChange(-1);
        }
        else
        {
            //否则3体力
            if (AP < 3) ApChange(3-AP);
        }
        if (saveAp != 0)
        {
            ApChange(saveAp);
            saveAp = 0;
        }
    }

    public void BurnDamageIn()
    {
        if (burnDamage != 0)
        {
            HpChange(-burnDamage,2);
            cardEffect.TakeBurn(type == PlayerType.player1);
            burnDamage = 0;
        }
    }

    //双方各抽4张牌
    public void AllGet4Card()
    {
        //我方抽卡，播放动画
        cardCurved.GetCards();
        cardCurved.GetCards();
        cardCurved.GetCards();
        cardCurved.GetCards();
        cardCurved.AddCardAnimations();

        //对方抽卡，播放动画
        enemyHCurved.HCNumChange(4);
    }
}

//卡种记号
public enum PlayerType
{
    player1,
    player2
}

