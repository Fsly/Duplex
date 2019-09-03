using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHCurved : MonoBehaviour
{
    //对方手牌管理
    public int handCardNum;//手牌数量
    public GameObject backCard; //手牌物体(背面)预制体

    public List<GameObject> ListHandCard = new List<GameObject>(); //手牌列表

    //出牌类
    public EnemyShowCard showingCard;

    public int NextCardNo;//下一张手牌的手牌号

    public bool isAbandonment;//是否弃牌

    private RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        NextCardNo = 0;
        isAbandonment = false;

        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
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
        if (Input.GetKeyDown(KeyCode.K))
        {

            RandomDestroyCard();
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
                GOAPBall.GetComponent<EnemyCardManager>().handCardNo = NextCardNo;
                NextCardNo++;
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

    //移除指定牌
    public void DestroyTheCard(int DesCardNo)
    {

        for (int i = 0; i < ListHandCard.Count; i++)
        {
            if (ListHandCard[i].GetComponent<EnemyCardManager>().handCardNo == DesCardNo)
            {
                Destroy(ListHandCard[i]);

                //将删除的手牌从列表移除
                ListHandCard.Remove(ListHandCard[i]);

                return;
            }
        }
    }

    //随机弃牌
    public void RandomDestroyCard()
    {
        int r = Random.Range(0, ListHandCard.Count);

        Destroy(ListHandCard[r]);

        //将删除的手牌从列表移除
        ListHandCard.Remove(ListHandCard[r]);
    }

    //弃牌
    public void AbandonmentCard()
    {
        if (ListHandCard.Count > 5)
        {
            isAbandonment = true;
            print("还需弃 " + (ListHandCard.Count - 5) + " 张牌");
        }
        else
        {
            isAbandonment = false;
            roundManager.EndingRoundStart();
        }
    }
}
