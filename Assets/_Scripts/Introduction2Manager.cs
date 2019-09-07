using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduction2Manager : MonoBehaviour
{
    //英雄觉醒介绍框管理
    //介绍框隐藏/更新

    public Text IntroductionT2;

    //卡牌信息
    public int cardNo;//卡号
    public Hero hero;
    public Awake awake;

    public HeroCardManager heroCardManager;
    public AwakeCardManager awakeCardManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateForHero()
    {
        //更新英雄内容
        IntroductionT2.text = "卡名：" + hero.monicker + "\nHP：" + hero.MaxHP
            + "\n介绍：" + hero.introduction + "\n效果：" + hero.effect;
    }

    public void UpdateForAwake()
    {
        //更新觉醒内容
        IntroductionT2.text = "卡名：" + awake.monicker
            + "\n介绍：" + awake.introduction + "\n效果：" + awake.effect;
    }
}
