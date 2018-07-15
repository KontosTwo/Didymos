using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StressManager : MonoBehaviour {
    [SerializeField]
    [Range(0,1f)]
    private float stressResistance;
    [SerializeField]
    private float stressStamina;

    [SerializeField]
    private float suppressiveFirePowerStress;
    [SerializeField]
    private float suppressiveFireDistanceModifiers;
    [SerializeField]
    private int suppressiveFireStressDuration;

    [SerializeField]
    private float coverDisparityModifier;


    private List<ChronicStressor> chronicStressors;
    private Dictionary<HumanoidModel, ChronicStressor> sightStressors;
    private Dictionary<HumanoidModel, ChronicStressor> friendStressors;


    private float rawStress;

    public virtual void Awake()
	{
        chronicStressors = new List<ChronicStressor>();
        sightStressors = new Dictionary<HumanoidModel, ChronicStressor>();
        friendStressors = new Dictionary<HumanoidModel, ChronicStressor>();
	}

	// Use this for initialization
	public virtual void Start () {
        InvokeRepeating("Destress", 1f, 1f);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        rawStress = rawStress < 0 ? 0 : rawStress;

	}

    public void SuppressiveFireStress(float power, float distance)
    {
        //Debug.Log(power);
        AddAcuteStressor(
            StressorFactory.CreateSuppressiveFireStressor(power, 
                                                          distance, 
                                                          suppressiveFirePowerStress, 
                                                          suppressiveFireDistanceModifiers)
        );
    }

    public void PendSightStress(float coverDisparity,HumanoidModel aggressor){
        ChronicStressor stressor = null;
        sightStressors.TryGetValue(aggressor, out stressor);
        if(stressor == null){
            stressor =
                StressorFactory.CreateSightStressor(coverDisparity, coverDisparityModifier);
            sightStressors.Add(aggressor, stressor);
            AddChronicStressor(stressor);
        }else{
            ChronicStressor newStressor =
                StressorFactory.CreateSightStressor(coverDisparity, coverDisparityModifier);
            stressor.ModifySeverity(newStressor.GetSeverity());
        }
    }

    public void StopSightStress(HumanoidModel aggressor){
        ChronicStressor stressor = null;
        sightStressors.TryGetValue(aggressor, out stressor);
        if (stressor != null)
        {
            RemoveChronicStressor(stressor);
        }
        sightStressors.Remove(aggressor);
    }

    protected float GetStress(){
        float chronicStress = 0;
        foreach(ChronicStressor stressor in chronicStressors){
            chronicStress += (stressor.GetSeverity() * stressResistance);
        }
        return rawStress + chronicStress;
    }

    protected void AddAcuteStressor(AcuteStressor stressor){
        rawStress += (stressor.GetSeverity() * stressResistance);
    }

    protected void AddChronicStressor(ChronicStressor stressor)
    {
        chronicStressors.Add(stressor);
    }

    protected void RemoveChronicStressor(ChronicStressor stressor)
    {
        chronicStressors.Remove(stressor);
    }

    public void Destress(){
        rawStress -= stressStamina;
    }
}
