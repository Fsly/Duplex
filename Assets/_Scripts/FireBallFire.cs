using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallFire : MonoBehaviour
{
    //我方使用流星雨按钮

    public int addDamage;//附加伤害

    public Text damageText;//附加伤害显示

    private ShowingCard showingCard;//出牌组件

    // Start is called before the first frame update
    void Start()
    {
        addDamage = 0;

        showingCard = GameObject.Find("ShowCardParent").GetComponent<ShowingCard>();
    }

    //弃牌加伤害
    public void AddtheDamage()
    {
        addDamage++;
        damageText.text = "流星雨 +" + addDamage;
    }

    //点击按钮发射
    public void Fire()
    {
        showingCard.attackCard.Damage += addDamage;
        showingCard.showCardManager.init();
        showingCard.AttackAction();
        showingCard.delayAttack = false;
    }
}
