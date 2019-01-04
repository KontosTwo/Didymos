using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/*
 * governs any passive interaction between humanoids that will
 * be checked every tick
 */
public class HumanoidInteractions : MonoBehaviour {

    private AmyModel amyModel;
    private ChanionModel chanionModel;
    // Replace with weapon-specific soldiers
    private List<AIHumanoidModel> enemiesModels;
    private List<EnemyMarker> enemyMarkers;

	// Use this for initialization
	void Start () {
        amyModel = HumanoidStore.GetAmyModel();
        chanionModel = HumanoidStore.GetChanionModel();
        enemiesModels = HumanoidStore.GetEnemiesModels();
        enemyMarkers = EnemyMarkerStore.GetEnemyMarkers();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        SeeInteractions();

        ClosenessInteraction();
        SeesEnemyMarker();
	}

    private void SeeInteractions(){
        for (int i = 0; i < 100; i ++)
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

    private void SeesEnemyMarker(){
        foreach(AIHumanoidModel enemy in enemiesModels){
            enemy.EffectCheckEnemyMarkers();
        }
    }
}
