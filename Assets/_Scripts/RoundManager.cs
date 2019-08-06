using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    //回合管理
    //回合周期
    //背景

    public GameObject StartUI;//开始的UI
    public Transform MainCanvas;//主画布

    //背景管理
    public Sprite[] BgSprite;
    public Image BgImage;
    public int BgNo;

    // Start is called before the first frame update
    void Start()
    {
        //随机场景
        RandomBackGround();
        //回合开始
        Instantiate(StartUI, MainCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (BgNo < BgSprite.Length - 1) BgNo += 1;
            else BgNo = 0;
            BgImage.sprite = BgSprite[BgNo];
        }
    }

    //随机背景
    void RandomBackGround()
    {
        BgNo = Random.Range(0, BgSprite.Length);
        BgImage.sprite = BgSprite[BgNo];
    }
}
