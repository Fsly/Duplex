using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardManager : MonoBehaviour
{
    //英雄牌实例
    //英雄属性及显示

    public Hero hero;

    public Image heroImage;
    public Text monickerText;
    public Text maxHPText;

    public void Init()
    {
        //卡面载入
        heroImage.sprite = hero.sprite;
        monickerText.text = hero.monicker;
        maxHPText.text = "" + hero.MaxHP;
    }
}
