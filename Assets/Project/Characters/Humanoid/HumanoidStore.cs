﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidStore : MonoBehaviour {

    [SerializeField]
    private GameObject amyObject;
    [SerializeField]
    private GameObject chanionObject;
    [SerializeField]
    private GameObject enemiesObject;

    private AmyModel amyModel;
    private ChanionModel chanionModel;
    // Replace with weapon-specific soldiers
    private List<AIHumanoidModel> enemiesModels;

    private static HumanoidStore instance;


    void Awake()
    {
        GameObject amy = amyObject.transform.GetChild(0).gameObject;
        amyModel = amyObject.GetComponentInChildren<AmyModel>();

        GameObject chanion = chanionObject.transform.GetChild(0).gameObject;
        chanionModel = chanionObject.GetComponentInChildren<ChanionModel>();

        enemiesModels = new List<AIHumanoidModel>();
        foreach (Transform enemy in enemiesObject.transform)
        {
            AIHumanoidModel model = enemy.GetComponent<AIHumanoidModel>();
            if (model != null)
            {
                enemiesModels.Add(model);
            }
        }

        instance = this;
    }

    public static AmyModel GetAmyModel(){
        return instance.amyModel;
    }

    public static ChanionModel GetChanionModel()
    {
        return instance.chanionModel;
    }

    public static List<AIHumanoidModel> GetEnemiesModels(){
        return instance.enemiesModels;
    }
}
