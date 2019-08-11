using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoundOver : MonoBehaviour
{
    public Transform ROImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickToEnd()
    {
        ROImage.DORotate(new Vector3(0f, 0f, 720f), 0.6f, RotateMode.FastBeyond360);
    }
}
