using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHCurved : MonoBehaviour
{
    //对方手牌管理
    public int handCardNum;//手牌数量
    public GameObject backCard; //手牌物体(背面)

    public List<GameObject> ListHandCard = new List<GameObject>(); //手牌列表

    //出牌类
    public EnemyShowCard showingCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HCNumChange(1);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            HCNumChange(-1);
        }
    }

    //生成等量的手牌
    public void HCNumChange(int addNum)
    {
        if (-addNum > handCardNum) addNum = -handCardNum;
        handCardNum += addNum;
        if (addNum > 0)
        {
            for (int i = 0; i < addNum; i++)
            {
                GameObject GOAPBall = Instantiate(backCard) as GameObject;
                GOAPBall.transform.parent = transform;
                ListHandCard.Add(GOAPBall);
                GOAPBall.GetComponent<EnemyCardManager>().EnterAnimation();
            }
        }
        else if (addNum < 0)
        {
            for (int i = 0; i < -addNum; i++)
            {
                Destroy(ListHandCard[0]);
                ListHandCard.Remove(ListHandCard[0]);
            }
        }
    }
}
