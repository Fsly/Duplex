using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoundStart : MonoBehaviour
{
    //回合开始动画

    public float keepTime = 0.5f;
    public float destroyTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
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
                    Destroy(gameObject);
                });
            });
        });
    }
}
