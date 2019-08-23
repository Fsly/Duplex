using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoundStart : MonoBehaviour
{
    //回合开始动画

    public float keepTime = 0.5f;
    public float destroyTime = 1.0f;

    private RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        //物体获取
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();

        UIAnimation();
    }

    void UIAnimation()
    {
        //回合开始动画
        transform.localScale = new Vector3(2, 1, 1);
        transform.DOScale(Vector3.one, keepTime).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1, 1, 1), destroyTime).OnComplete(() =>
            {
                transform.DOScale(new Vector3(1, 0, 1), 0.2f).OnComplete(() =>
                {
                    if (roundManager.roundPhase == RoundPhase.Preparatory)
                        roundManager.DrawRoundStart();
                    else if (roundManager.roundPhase == RoundPhase.Ending)
                    {
                        roundManager.isMyturn = !roundManager.isMyturn;
                        roundManager.roundNum++;
                        roundManager.PreparatoryRoundStart();
                    }

                    Destroy(gameObject);
                });
            });
        });
    }
}
