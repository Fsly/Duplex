using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    //回合周期
    public GameObject StartUI;//开始的UI
    public Transform MainCanvas;//主画布

    // Start is called before the first frame update
    void Start()
    {
        //回合开始
        Instantiate(StartUI, MainCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
