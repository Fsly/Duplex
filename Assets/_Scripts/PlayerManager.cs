using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //玩家类
    //用于双方头像框UI
    //实现HP和AP动画（暂未）

    public Hero hero;
    public Awake awake;

    public int HP;
    public int AP;

    //UI
    public Text HPText;
    public Slider HPSlider;
    public GameObject APBar;
    public Image heroHeadImage;

    public List<GameObject> ListAPBall = new List<GameObject>(); //手牌列表 

    //Prefab
    public GameObject APBall;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject GOAPBall = Instantiate(APBall) as GameObject;
            GOAPBall.transform.parent = APBar.transform;
            ListAPBall.Add(GOAPBall);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Destroy(ListAPBall[0]);
            ListAPBall.Remove(ListAPBall[0]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HP -= 1;
            HPText.text = HP + "/" + hero.MaxHP;
            if (HP >= 0) HPSlider.value = (float)HP / (float)hero.MaxHP;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HP += 1;
            HPText.text = HP + "/" + hero.MaxHP;
            if (HP <= hero.MaxHP) HPSlider.value = (float)HP / (float)hero.MaxHP;
        }
    }

    public void init()
    {
        HP = hero.MaxHP;
        HPText.text = HP + "/" + hero.MaxHP;
        HPSlider.value = 1f;
        heroHeadImage.sprite = hero.headSprite;
    }
}
