using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class NetTest : MonoBehaviour
{
    //socket和缓冲区
    Socket socket;
    const int BUFFER_SIZE = 1024;
    public byte[] readBuff = new byte[BUFFER_SIZE];

    //消息列表
    List<string> msgList = new List<string>();

    //自己的IP和端口
    string id;

    //玩家列表
    Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    public Hero hero;
    public Awake awake;
    public int GetCardNum;

    //发送选人协议
    void SendSelectHeroAwake()
    {
        //组装准备时选择英雄和觉醒的协议并发送
        //协议示例：SHA 127.0.0.1:3564 1 2

        //组装协议
        string str = "SHA ";
        str += id + " ";
        str += hero.No + " ";
        str += awake.No + " ";

        //发送
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);
        Debug.Log("发送 " + str);
    }

    //发送抽牌协议
    void SendGetCard()
    {
        //组装玩家离开位置的协议并发送
        //协议示例：GETCARD 127.0.0.1:3564 2

        //组装协议
        string str = "GETCARD ";
        str += id + " ";
        str += GetCardNum + " ";

        //发送
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);
        Debug.Log("发送 " + str);
    }
}
