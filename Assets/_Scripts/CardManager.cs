using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    //手牌实例：手牌点击功能实现，手牌属性

    public CounterCard counterCard;
    public AttackCard attackCard;

    public Image attackImage;
    public Image attackBallImage;
    public Image counterImage;
    public Image counterBallImage;
    public Image damageCubeImage;

    public Text attackPointText;
    public Text counterPointText;
    public Text damageText;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //卡面载入
    private void init()
    {
        attackImage.sprite = attackCard.attackSprite;
        attackBallImage.sprite = attackCard.attackBallSprite;
        damageCubeImage.sprite = attackCard.damageCubeSprite;
        attackPointText.text = attackCard.ActionPoint + "";
        damageText.text = attackCard.Damage + "";

        counterImage.sprite = counterCard.counterSprite;
        counterBallImage.sprite = counterCard.counterBallSprite;
        counterPointText.text = counterCard.ActionPoint + "";
    }
}
