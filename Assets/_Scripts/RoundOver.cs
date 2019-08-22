using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoundOver : MonoBehaviour
{
    //结束图片
    public Transform ROImage;

    //回合管理类
    private RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickToEnd()
    {
        ROImage.DORotate(new Vector3(0f, 0f, 720f), 0.6f, RotateMode.FastBeyond360);

        if (roundManager.roundPhase == RoundPhase.Main
            && roundManager.waitCounter == WaitPhase.NoWait)
        {
            roundManager.AbandonmentRoundStart();
        }
    }
}
