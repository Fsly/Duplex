using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonGet : MonoBehaviour
{
    //技能按钮

    

    //技能postion
    public Transform t_HeroSkill;
    public Transform t_AwakeSkill;

    //按钮预制体
    public GameObject activeSkillGO;
    public GameObject passtiveSkillGO;
    public GameObject cannotSkillGO;
    public GameObject awakeSkillGO;

    
    private PlayerManager playerManager;//玩家英雄
    private SkillManager skillManager;
    private RoundManager roundManager;//回合管理

    //主动技能的英雄
    int[] activeHeroNo =
    {
        1
    };

    //被动技能的英雄
    int[] passtiveHeroNo =
    {
        2,3,4
    };

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("MainUI").GetComponent<PlayerManager>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    public void InstantiateSkillButton()
    {
        if (Array.IndexOf(activeHeroNo, playerManager.hero.No) != -1)
        {
            //主动技能
            GameObject GO_as = Instantiate(activeSkillGO, t_HeroSkill) as GameObject;
            GO_as.transform.position = t_HeroSkill.position;
            GO_as.GetComponentsInChildren<Text>()[0].text = playerManager.hero.monicker;
            GO_as.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                if (playerManager.hero.No == 1 &&
                playerManager.HeroTimer > 0 &&
                roundManager.roundPhase == RoundPhase.Main &&
                roundManager.isMyturn)
                {
                    SkillManager.printSkill("我方挑战者使用技能");
                    playerManager.HeroTimer = 0;
                    skillManager.SkillEffect(1, true);
                }
            });
        }
        else if (Array.IndexOf(passtiveHeroNo, playerManager.hero.No) != -1)
        {
            //被动技能
            GameObject GO_ps = Instantiate(passtiveSkillGO, t_HeroSkill) as GameObject;
            GO_ps.transform.position = t_HeroSkill.position;
            GO_ps.GetComponentsInChildren<Text>()[0].text = playerManager.hero.monicker;
        }
        GameObject GO_cs = Instantiate(cannotSkillGO, t_AwakeSkill) as GameObject;
        GO_cs.transform.position = t_AwakeSkill.position;
        GO_cs.GetComponentsInChildren<Text>()[0].text = playerManager.awake.monicker;
    }

    public void AwakeReady()
    {
        Destroy(t_AwakeSkill.GetChild(0).gameObject);
        GameObject GO_as = Instantiate(awakeSkillGO, t_AwakeSkill) as GameObject;
        GO_as.transform.position = t_AwakeSkill.position;
        GO_as.GetComponentsInChildren<Text>()[0].text = playerManager.awake.monicker;
    }
}
