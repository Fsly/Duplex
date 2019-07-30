using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroButton : MonoBehaviour
{
    //选择按钮
    //用于英雄卡和觉醒卡实例
    //点击生成一个确定按钮

    //点击事件
    public HandCardButton cardButton;//产生何种按钮，不影响功能
    public GameObject heroButton;
    public GameObject awakeButton;

    // Start is called before the first frame update
    void Start()
    {
        
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
        GOCardButton.transform.position = Input.mousePosition;
        GOCardButton.transform.parent = transform;
        GOCardButton.GetComponent<ActionButton>().selectHeroButton = this;
    }

    public void UseCard()
    {
        print("aini");
    }
}
