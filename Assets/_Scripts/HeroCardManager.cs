using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //英雄牌实例
    //英雄属性及显示

    public Hero hero;

    public Image heroImage;
    public Text monickerText;
    public Text maxHPText;

    public GameObject GOIntroduction2;

    // Start is called before the first frame update
    void Start()
    {
        GOIntroduction2.transform.localScale = Vector3.zero;
    }

    public void Init()
    {
        //卡面载入
        heroImage.sprite = hero.sprite;
        monickerText.text = hero.monicker;
        maxHPText.text = "" + hero.MaxHP;
    }

    // 鼠标进入下方显示介绍
    public void OnPointerEnter(PointerEventData eventData)
    {
        GOIntroduction2.transform.localScale = Vector3.one;
        GOIntroduction2.GetComponent<Introduction2Manager>().hero = hero;
        GOIntroduction2.GetComponent<Introduction2Manager>().UpdateForHero();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GOIntroduction2.transform.localScale = Vector3.zero;
    }
}
