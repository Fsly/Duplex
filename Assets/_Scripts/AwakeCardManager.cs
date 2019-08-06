using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AwakeCardManager : MonoBehaviour
{
    //觉醒牌实例
    //觉醒属性及显示

    public Awake awake;

    public Image awakeImage;
    public Text monickerText;

    public void Init()
    {
        //卡面载入
        awakeImage.sprite = awake.sprite;
        monickerText.text = awake.monicker;
    }
}
