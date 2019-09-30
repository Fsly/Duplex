using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeadIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //公共介绍框UI
    private Introduction3Manager introduction3Manager;

    //玩家
    public PlayerManager playerManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入显示角色信息
        introduction3Manager.hero = playerManager.hero;
        introduction3Manager.awake = playerManager.awake;
        if (playerManager.awakeIsOpen) introduction3Manager.UpdateForDisplay();
        else introduction3Manager.UpdateForDisplay2();
        introduction3Manager.DisplayIntroduction();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开范围
        if (eventData.IsPointerMoving())
            introduction3Manager.HiddenIntroduction();
    }

    // Start is called before the first frame update
    void Start()
    {
        //物体获取
        introduction3Manager = GameObject.Find("Introduction3").GetComponent<Introduction3Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
