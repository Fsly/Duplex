using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AwakeCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //觉醒牌实例
    //觉醒属性及显示

    public Awake awake;

    public Image awakeImage;
    public Text monickerText;

    public GameObject GOIntroduction2;

    // Start is called before the first frame update
    void Start()
    {
        GOIntroduction2.transform.localScale = Vector3.zero;
    }

    public void Init()
    {
        //卡面载入
        awakeImage.sprite = awake.sprite;
        monickerText.text = awake.monicker;
    }

    // 鼠标进入下方显示介绍
    public void OnPointerEnter(PointerEventData eventData)
    {
        GOIntroduction2.transform.localScale = Vector3.one;
        GOIntroduction2.GetComponent<Introduction2Manager>().awake = awake;
        GOIntroduction2.GetComponent<Introduction2Manager>().UpdateForAwake();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GOIntroduction2.transform.localScale = Vector3.zero;
    }
}
