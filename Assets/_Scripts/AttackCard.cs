using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : MonoBehaviour
{
    //进攻卡
    public int attackCardNo;//进攻卡号
    public string CardName;//卡名
    public string CardIntroduction;//卡介绍
    public string CardEffect;//卡效果

    //逻辑层面
    public Attribute counterAttribute;//卡种类为物理or魔法
    public int ActionPoint;//行动点
    public Attribute damageAttribute;//伤害为物理or魔法
    public int Damage; //伤害
    public bool canCounter = true; //可被反击

    //UI层面
    public Sprite attackSprite; //图片
    public Sprite attackBallSprite; //行动点球
    public Sprite damageCubeSprite; //伤害方块

    public AttackCard(AttackCard card)
    {
        attackCardNo = card.attackCardNo;
        CardName = card.CardName;
        CardIntroduction = card.CardIntroduction;
        CardEffect = card.CardEffect;
        counterAttribute = card.counterAttribute;
        ActionPoint = card.ActionPoint;
        damageAttribute = card.damageAttribute;
        Damage = card.Damage;
        attackSprite = card.attackSprite;
        attackBallSprite = card.attackBallSprite;
        damageCubeSprite = card.damageCubeSprite;
        canCounter = card.canCounter;
    }

}

public enum Attribute
{
    Physical,
    Magic
}
