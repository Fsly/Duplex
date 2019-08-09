using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GetCard : MonoBehaviour
{
    float radian = 0;  // 弧度
    float perRadian = 0.08f; // 每次变化的弧度
    float radius = 10f; // 半径
    Vector3 oldPos; // 开始时候的坐标
    CardCurved cardCurved;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    private void Init()
    {
        //将最初的位置保存到oldPos
        oldPos = transform.position;
        cardCurved = GameObject.Find("HandCardPrefab").GetComponent<CardCurved>();
    }

    // Update is called once per frame
    void Update()
    {
        CardFloat();
    }

    //浮动效果
    void CardFloat()
    {
        //弧度每次加0.03
        radian += perRadian;
        //dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
        float dy = Mathf.Cos(radian) * radius;
        transform.position = oldPos + new Vector3(0, dy, 0);
    }

    //点击抽卡
    public void ClickToGet2Card()
    {
        cardCurved.GetCards();
        cardCurved.GetCards();
        cardCurved.AddCardAnimations();
        StartCoroutine(ToGet2Card());
        Destroy(gameObject);
    }

    IEnumerator ToGet2Card()
    {
        yield return new WaitForSeconds(0.6f);
        cardCurved.GetCards();
        cardCurved.AddCardAnimations();
    }
}
