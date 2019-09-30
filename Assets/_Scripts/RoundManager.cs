using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoundManager : MonoBehaviour
{
    //回合管理
    //回合周期
    //背景

    public GameObject StartUI; //我方开始的UI
    public GameObject EndingUI; //我方结束的UI
    public GameObject EnemyStartUI;//对方开始的UI
    public GameObject EnemyEndingUI; //对方结束的UI
    public GameObject getCardPrefabs;//抽牌预制体
    public GameObject waitPrefab; //等待预制体
    public GameObject NoCounterPrefab;//不出反击牌预制体
    public Transform MainCanvas;//主画布
    public GameObject discardTipPrefab; //弃牌数提示框

    //背景管理
    public Sprite[] BgSprite;
    public Image BgImage;
    public int BgNo;

    //本局状态
    public int roundNum; //回合计数
    public bool isMyturn;//谁的回合
    public RoundPhase roundPhase; //当前阶段
    public WaitPhase waitCounter; //是否等待反击

    public PlayerManager myPlayer;//我方玩家类
    public PlayerManager EnemyPlayer;//对方玩家类
    public SkillButtonGet skillButtonGet;//我方技能

    private CardCurved cardCurved;
    private EnemyHCurved enemyHCurved;
    private SkillManager skillManager;

    private Vector3 waitFirstSet;

   public Text RoundNumText;

    // Start is called before the first frame update
    void Start()
    {
        //随机场景
        RandomBackGround();
    }

    public void GameStartReady()
    {
        //初始化赋值
        RoundGoingStart();

        //目前设定我方先开，所有进入我方准备阶段
        PreparatoryRoundStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (BgNo < BgSprite.Length - 1) BgNo += 1;
            else BgNo = 0;
            BgImage.sprite = BgSprite[BgNo];
        }
    }

    //随机背景
    void RandomBackGround()
    {
        BgNo = Random.Range(0, BgSprite.Length);
        BgImage.sprite = BgSprite[BgNo];
    }

    //游戏初始化赋值
    public void RoundGoingStart()
    {
        roundPhase = RoundPhase.Preparatory;
        waitCounter = WaitPhase.NoWait;
        roundNum = 0;

        myPlayer = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        EnemyPlayer = GameObject.Find("EnemyUI").GetComponent<PlayerManager>();
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
        enemyHCurved = GameObject.Find("EnemyHCPrefab").GetComponent<EnemyHCurved>();
        RoundNumText = GameObject.Find("RoundNumText").GetComponent<Text>();
        skillButtonGet = GameObject.Find("MainUI").GetComponent<SkillButtonGet>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
    }

    //准备阶段初始化
    public void PreparatoryRoundStart()
    {
        roundPhase = RoundPhase.Preparatory;

        //回合增加，显示更新
        roundNum++;
        if (roundNum < 10) RoundNumText.text = "Round : 0" + roundNum;
        else RoundNumText.text = "Round : " + roundNum;

        //回合开始动画,回复行动点
        if (isMyturn)
        {
            Instantiate(StartUI, MainCanvas);
            myPlayer.ApGetToStart();
            myPlayer.BurnDamageIn();
        }
        else
        {
            Instantiate(EnemyStartUI, MainCanvas);
            EnemyPlayer.ApGetToStart();
            EnemyPlayer.BurnDamageIn();
            WaitPrefabOn();
        }

        //挑战者技能
        if (myPlayer.hero.No == 1)
        {
            SkillManager.printSkill("我方挑战者恢复计数器");
            
            myPlayer.HeroTimer++;
        }
        else if (EnemyPlayer.hero.No == 1)
        {
            SkillManager.printSkill("对方挑战者恢复计数器");
            EnemyPlayer.HeroTimer++;
        }
    }

    //抽牌阶段初始化
    public void DrawRoundStart()
    {
        roundPhase = RoundPhase.Draw;

        if (isMyturn)
        {
            Instantiate(getCardPrefabs, MainCanvas);
        }
    }

    //主要阶段初始化
    public void MainRoundStart()
    {
        roundPhase = RoundPhase.Main;
    }

    //弃牌阶段初始化
    public void AbandonmentRoundStart()
    {
        roundPhase = RoundPhase.Abandonment;

        if (isMyturn)
        {
            Instantiate(discardTipPrefab, MainCanvas); 
            cardCurved.AbandonmentCard();
        }
        else
        {
            enemyHCurved.AbandonmentCard();
        }
    }

    //结束阶段初始化
    public void EndingRoundStart()
    {
        roundPhase = RoundPhase.Ending;

        //发动效果
        //光之猎手技能发动
        if (myPlayer.hero.No == 4)
        {
            if (myPlayer.HeroTimer > 0)
            {
                SkillManager.printSkill("我方光之猎手使用技能");
                myPlayer.HeroTimer = 0;
                skillManager.SkillEffect(4, true);
            }
        }
        else if (EnemyPlayer.hero.No == 4)
        {
            if (EnemyPlayer.HeroTimer > 0)
            {
                SkillManager.printSkill("对方光之猎手使用技能");
                EnemyPlayer.HeroTimer = 0;
                skillManager.SkillEffect(4, false);
            }
        }

        //回合结束动画
        if (isMyturn)
        {
            Instantiate(EndingUI, MainCanvas);
        }
        else
        {
            Instantiate(EnemyEndingUI, MainCanvas);
            WaitPrefabOff();
        }
    }

    //等待对方
    public void WaitingEnemy()
    {
        waitCounter = WaitPhase.WaitEnemy;
    }

    //等待结束
    public void EnemyWaitOK()
    {
        waitCounter = WaitPhase.NoWait;
    }

    //对方回合，等待我方打出反击牌
    public void WaitingMe()
    {
        waitCounter = WaitPhase.WaitMe;

        Instantiate(NoCounterPrefab, MainCanvas);
    }

    //我方打出反击牌，等待结束
    public void MeWaitOK()
    {
        waitCounter = WaitPhase.NoWait;
    }

    //"等待对方"提示框出现
    public void WaitPrefabOn()
    {
        waitFirstSet = waitPrefab.transform.position;
        Transform t_UI = GameObject.Find("WaitInit").transform;
        waitPrefab.transform.DOMove(t_UI.position, 0.8f).SetEase(Ease.OutBack);
    }

    //"等待对方"提示框隐藏
    public void WaitPrefabOff()
    {
        waitPrefab.transform.DOMove(waitFirstSet, 0.8f).SetEase(Ease.OutBack);
    }
}

//阶段:准备,抽牌,主要,弃牌,结束
public enum RoundPhase
{
    Preparatory,
    Draw,
    Main,
    Abandonment,
    Ending
}

//等待阶段
public enum WaitPhase
{
    NoWait,
    WaitMe,
    WaitEnemy
}

