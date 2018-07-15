using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * governs any passive interaction between humanoids that will
 * be checked every tick
 */
public class HumanoidInteractions : MonoBehaviour {

    private AmyModel amyModel;
    private ChanionModel chanionModel;
    // Replace with weapon-specific soldiers
    private List<HumanoidModel> enemiesModels;

	// Use this for initialization
	void Start () {
        amyModel = HumanoidStore.GetAmyModel();
        chanionModel = HumanoidStore.GetChanionModel();
        enemiesModels = HumanoidStore.GetEnemiesModels();
	}
	
	// Update is called once per frame
	void Update () {
        SeeInteractions();
        ClosenessInteraction();
	}

    private void SeeInteractions(){
        foreach(HumanoidModel enemy in enemiesModels){
            if(enemy.InfoCanSee(amyModel)){
                enemy.EffectOnSeeEnemy(amyModel);
            }else{
                enemy.EffectDoesNotSeeEnemy(amyModel);
            }/*
            if (amyModel.InfoCanSee(enemy)){
                amyModel.EffectOnSeeEnemy(enemy);
            }else{
                amyModel.EffectDoesNotSeeEnemy(enemy);
            }

            if (enemy.InfoCanSee(chanionModel)){
                enemy.EffectOnSeeEnemy(chanionModel);
            }else{
                enemy.EffectDoesNotSeeEnemy(chanionModel);
            }
            if (chanionModel.InfoCanSee(enemy)){
                chanionModel.EffectOnSeeEnemy(enemy);
            }else{
                chanionModel.EffectDoesNotSeeEnemy(enemy);
            }*/
        }
    }

    private void ClosenessInteraction(){
        foreach (HumanoidModel enemy in enemiesModels){
            foreach (HumanoidModel enemysFriend in enemiesModels){
                if(enemy != enemysFriend){
                    enemy.EffectBenefitFromCloseness(enemysFriend);
                }
            }
        }

        amyModel.EffectBenefitFromCloseness(chanionModel);
        chanionModel.EffectBenefitFromCloseness(amyModel);
    }
}
