using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PointMask : MonoBehaviour
{
    public Transform pointTransform;

    //出现
    public void OnPointAdd()
    {
        pointTransform.localScale = Vector3.zero;
        pointTransform.DOScale(new Vector3(1, 1, 0), 0.2f);
    }

    //消失
    public void OnPointExit()
    {
        pointTransform.DOScale(Vector3.zero, 0.2f);
        Destroy(gameObject, 0.3f);
    }
}
