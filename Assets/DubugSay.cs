using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DubugSay : MonoBehaviour
{
    public GameObject[] debugButton;

    public Button b_xuanren;
    public Button b_choupai;
    public Button b_chupai;
    public Button b_jieshu;
    public Button b_qipai;
    public Button b_tuichu;
    public Button b_tiaoshi;

    private void Start()
    {
        for (int i = 0; i < debugButton.Length; i++)
        {
                debugButton[i].transform.localScale = Vector3.zero;
        }
    }

    public void TiaoShi()
    {
        for (int i = 0; i < debugButton.Length; i++)
        {
            if (debugButton[i].transform.localScale == Vector3.zero)
            {
                debugButton[i].transform.localScale = Vector3.one;
            }
            else if (debugButton[i].transform.localScale == Vector3.one)
            {
                debugButton[i].transform.localScale = Vector3.zero;
            }
        }
    }


}
