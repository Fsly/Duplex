﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonGet : MonoBehaviour
{
    //玩家英雄
    private PlayerManager playerManager;

    //技能postion
    public Transform t_HeroSkill;
    public Transform t_AwakeSkill;

    //按钮预制体
    public GameObject activeSkillGO;
    public GameObject passtiveSkillGO;
    public GameObject cannotSkillGO;
    public GameObject awakeSkillGO;

    int[] activeHeroNo =
    {
        1
    };

    int[] passtiveHeroNo =
    {
        2,3,4
    };

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("MainUI").GetComponent<PlayerManager>();
    }

    public void InstantiateSkillButton()
    {
        if (Array.IndexOf(activeHeroNo, playerManager.hero.No) != -1)
        {
            //主动技能
            GameObject GO_as= Instantiate(activeSkillGO, t_HeroSkill) as GameObject;
            GO_as.transform.position = t_HeroSkill.position;
        }
        else if (Array.IndexOf(passtiveHeroNo, playerManager.hero.No) != -1)
        {
            //被动技能
            GameObject GO_as = Instantiate(passtiveSkillGO, t_HeroSkill) as GameObject;
            GO_as.transform.position = t_HeroSkill.position;
        }
    }
}
