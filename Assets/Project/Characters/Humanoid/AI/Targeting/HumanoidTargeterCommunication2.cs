using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidTargeterCommunication2 : MonoBehaviour {


    private static HumanoidTargeterCommunication2 instance;

    private List<HumanoidTargeter> targeters;


    // Use this for initialization
    private void Awake()
    {
        targeters = new List<HumanoidTargeter>();
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(addEnemyMarkerCommunicators.Count);
    }

    public static void AddBlackBoardSubscriber(HumanoidTargeter blackBoard)
    {
        instance.targeters.Add(blackBoard);
    }

    public static void CommunicateUpdate(HumanoidTargeter origin, EnemyMarker marker)
    {
        
    }

    public static void InterruptUpdate(HumanoidTargeter whoToInterrupt)
    {
        
    }



}
