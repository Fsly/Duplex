using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeroAwakeCurved : MonoBehaviour
{
    //选择界面管理
    //储存英雄和觉醒
    //实现选英雄选觉醒功能以及相关动画（暂未）

    public List<Hero> heroes;//英雄牌
    public List<Awake> awakes;//觉醒牌

    public List<Hero> S_heroes; //洗牌后英雄牌
    public List<Awake> S_awakes;  //洗牌后觉醒牌

    public GameObject[] heroPrefab; //英雄牌预制体
    public GameObject[] awakePrefab; //觉醒牌预制体

    public Transform heroPrefabInit;//英雄牌发牌位置
    public Transform awakePrefabInit;//觉醒牌发牌位置

    // Start is called before the first frame update
    void Start()
    {
        RandomHero();
        RandomAwake();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //英雄牌洗牌，取前三张生成预制体,相关动画
    public void RandomHero()
    {
        S_heroes = RandomSortList(heroes);
        Vector3[] targetPosition = new Vector3[3];
        for (int i = 0; i < 3; i++)
        {
            heroPrefab[i].GetComponent<HeroCardManager>().hero = S_heroes[i];
            heroPrefab[i].GetComponent<HeroCardManager>().init();
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
        for (int i = 0; i < 3; i++)
        {
            awakePrefab[i].GetComponent<AwakeCardManager>().awake = S_awakes[i];
            awakePrefab[i].GetComponent<AwakeCardManager>().init();
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
        print("英雄" + (select + 1));
        for (int i = 0; i < 3; i++)
        {
            if (i != select) heroPrefab[i].transform.DOScale(Vector3.zero, 0.2f);
        }
    }

    //选择此觉醒牌
    public void SelectAwake(int select)
    {
        print("觉醒" + (select + 1));
        for (int i = 0; i < 3; i++)
        {
            if (i != select) awakePrefab[i].transform.DOScale(Vector3.zero, 0.2f);
        }
    }
}
