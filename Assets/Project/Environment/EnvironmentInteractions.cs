using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Interactions between the Environment and other
 * entities
 */
public class EnvironmentInteractions : MonoBehaviour {
    private List<HumanoidModel> humanoidSubscribers;


    private static EnvironmentInteractions instance;
	// Use this for initialization

	private void Awake()
	{
        instance = this;
        humanoidSubscribers = new List<HumanoidModel>();
	}
	void Start () {
        humanoidSubscribers.Add(HumanoidStore.GetAmyModel());
        humanoidSubscribers.Add(HumanoidStore.GetChanionModel());
        humanoidSubscribers.AddRange(HumanoidStore.GetEnemiesModels());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void ProjectileInstantImpactEffect(Vector3 location, float radius, float power){
        List<HumanoidModel> subscribers = instance.humanoidSubscribers;
        foreach(HumanoidModel humanoid in subscribers){
            humanoid.EffectExperienceImpact(location, radius, power);
        }
    }
}
