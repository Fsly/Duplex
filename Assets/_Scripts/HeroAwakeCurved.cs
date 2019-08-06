using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeroAwakeCurved : MonoBehaviour
{
    //选择界面管理
    //实现选英雄选觉醒功能以及相关动画
    //准备按钮

    //储存英雄和觉醒
    public List<Hero> heroes;//英雄牌
    public List<Awake> awakes;//觉醒牌

    //各3个预制体
    public GameObject[] heroPrefab; //英雄牌预制体
    public GameObject[] awakePrefab; //觉醒牌预制体

    //发牌位置
    public Transform heroPrefabInit;//英雄牌发牌位置
    public Transform awakePrefabInit;//觉醒牌发牌位置

    //选定位置
    public Transform heroPrefabOver;//英雄牌选定位置
    public Transform awakePrefabOver;//觉醒牌选定位置

    //选号，未选为-1
    public int isHeroSelect;
    public int isAwakeSelect;

    //选中按钮
    public GameObject readyButton;

    //洗牌后
    private List<Hero> S_heroes; //洗牌后英雄牌
    private List<Awake> S_awakes;  //洗牌后觉醒牌

    // Start is called before the first frame update
    void Start()
    {
        OnStartDo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //初始化
    void OnStartDo()
    {
        //未选择英雄和觉醒
        isHeroSelect = -1;
        isAwakeSelect = -1;

        //随机抽3张英雄和觉醒
        RandomHero();
        RandomAwake();

        //隐藏按钮
        readyButton.SetActive(false);
    }

    //英雄牌洗牌，取前三张生成预制体,相关动画
    public void RandomHero()
    {
        S_heroes = RandomSortList(heroes);
        Vector3[] targetPosition = new Vector3[3];
        for (var i = 0; i < 3; i++)
        {
            heroPrefab[i].GetComponent<HeroCardManager>().hero = S_heroes[i];
            heroPrefab[i].GetComponent<HeroCardManager>().Init();
            heroPrefab[i].GetComponent<SelectHeroButton>().sequence = i;
            targetPosition[i] = heroPrefab[i].transform.position;
            heroPrefab[i].transform.position = heroPrefabInit.position;
            heroPrefab[i].transform.DOMove(targetPosition[i], 1);
        }
    }

    //觉醒牌洗牌，取前三张生成预制体,相关动画
    public void RandomAwake()
    {
        S_awakes = RandomSortList(awakes);
        Vector3[] targetPosition = new Vector3[3];
        for (var i = 0; i < 3; i++)
        {
            awakePrefab[i].GetComponent<AwakeCardManager>().awake = S_awakes[i];
            awakePrefab[i].GetComponent<AwakeCardManager>().Init();
            awakePrefab[i].GetComponent<SelectHeroButton>().sequence = i;
            targetPosition[i] = awakePrefab[i].transform.position;
            awakePrefab[i].transform.position = awakePrefabInit.position;
            awakePrefab[i].transform.DOMove(targetPosition[i], 1);
        }
    }

    //打乱List数组
    public List<T> RandomSortList<T>(List<T> ListT)
    {
        System.Random random = new System.Random();
        List<T> newList = new List<T>();
        foreach (T item in ListT)
        {
            newList.Insert(random.Next(newList.Count), item);
        }
        return newList;
    }

    //选择此英雄牌
    public void SelectHero(int select)
    {
        isHeroSelect = select;

        //隐藏其他牌
        for (var i = 0; i < 3; i++)
        {
            heroPrefab[i].GetComponent<SelectHeroButton>().cardButton = HandCardButton.Cannot;
            if (i != select)
            {
                heroPrefab[i].transform.DOScale(Vector3.zero, 0.2f);
                Destroy(heroPrefab[i], 0.3f);
            }
        }

        Setout();
    }

    //选择此觉醒牌
    public void SelectAwake(int select)
    {
        isAwakeSelect = select;

        //隐藏其他牌
        for (var i = 0; i < 3; i++)
        {
            awakePrefab[i].GetComponent<SelectHeroButton>().cardButton = HandCardButton.Cannot;
            if (i != select)
            {
                awakePrefab[i].transform.DOScale(Vector3.zero, 0.2f);
                Destroy(awakePrefab[i], 0.3f);
            }
        }

        Setout();
    }

    //判断准备，动画
    public void Setout()
    {
        if (isHeroSelect > -1 && isAwakeSelect > -1)
        {
            //牌的位置放于左右两侧
            heroPrefab[isHeroSelect].transform.DOMove(heroPrefabOver.transform.position, 1);
            awakePrefab[isAwakeSelect].transform.DOMove(awakePrefabOver.transform.position, 1);

            //准备按钮出现
            readyButton.SetActive(true);
        }
    }

    //准备按钮点击事件
    public void DoReadyButton()
    {
        Destroy(gameObject);
    }
}
