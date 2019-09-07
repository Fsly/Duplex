using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroButton : MonoBehaviour
{
    // 选择按钮
    // 用于英雄卡和觉醒卡实例
    // 点击生成一个确定按钮
    // 向选择界面管理发送请求，选择此牌

    // 点击事件标记（产生何种按钮，不影响功能）
    public HandCardButton cardButton;

    //预制体
    public GameObject heroButton;
    public GameObject awakeButton;

    //选择界面管理
    public HeroAwakeCurved heroAwakeCurved;

    //抽中位置
    public int sequence;

    // Start is called before the first frame update
    void Start()
    {
        heroAwakeCurved = GameObject.Find("HeroAwakePrefab").GetComponent<HeroAwakeCurved>();
    }

    public void ButtonInstantiate()
    {
        //点击卡牌根据情况生成按钮
        GameObject GOCardButton = null;
        if (cardButton == HandCardButton.Hero)
        {
            GOCardButton = Instantiate(heroButton) as GameObject;
        }
        else if (cardButton == HandCardButton.Awake)
        {
            GOCardButton = Instantiate(awakeButton) as GameObject;
        }
        if (cardButton != HandCardButton.Cannot)
        {
            GOCardButton.transform.position = Input.mousePosition;
            GOCardButton.transform.parent = transform;
            GOCardButton.GetComponent<ActionButton>().selectHeroButton = this;
        }
    }

    public void UseCard()
    {
        if (cardButton == HandCardButton.Hero)
        {
            heroAwakeCurved.SelectHero(sequence);
        }
        else if (cardButton == HandCardButton.Awake)
        {
            heroAwakeCurved.SelectAwake(sequence);
        }
    }
}
