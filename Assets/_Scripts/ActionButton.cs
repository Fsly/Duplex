using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerExitHandler
{
    //卡片按钮动画
    public CardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        //旋转出现
        transform.DORotate(new Vector3(0f, 0f, 360f), 0.3f, RotateMode.FastBeyond360);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        //点击事件
        cardManager.UseCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开删除按钮
        transform.DOScale(Vector3.zero, 0.2f);
        Destroy(gameObject, 0.3f);
    }
}
