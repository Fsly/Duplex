using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NotCounter : MonoBehaviour
{
    //不出反击牌按钮

    public RoundManager roundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //旋转出现
        transform.DORotate(new Vector3(0f, 0f, 360f), 0.3f, RotateMode.FastBeyond360);

        //物体获取
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    public void ButtonClick()
    {
        print("不打出反击");

        roundManager.MeWaitOK();

        Destroy(gameObject);
    }

    public void BeDestroy()
    {
        //删除按钮
        transform.DOScale(Vector3.zero, 0.2f);
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
