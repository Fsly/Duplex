using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    //Dotween用法

    // Start is called before the first frame update
    void Start()
    {
        //position
        //transform.DOMove(Vector3.one, 2);
        //transform.DOLocalMove(Vector3.one, 2);
        //transform.DOLocalMoveX(1, 2);

        //rotate
        //transform.DORotate(new Vector3(90f, 0, 0), 2);
        //transform.DOLocalRotate(new Vector3(90f, 0, 0), 2);
        //transform.DOLocalRotateQuaternion(Quaternion.Euler(90f, 90f, 90f), 2);
        //transform.DOLookAt(new Vector3(90f, 0, 0), 2);

        //scale
        //transform.DOScale(Vector3.one * 2, 2);

        //方向，力的大小；持续时间；震动次数；超出值
        transform.DOPunchPosition(new Vector3(0, 1, 0), 2, 2, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
