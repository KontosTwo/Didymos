using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommunicationPackage {
    private HashSet<CommunicatableEnemyMarker> payload;
    private HumanoidTargeter issuer;

    private HashSet<HumanoidTargeter> alreadyCommunicated;

    public int id;
    private static int counter = 0;

    public CommunicationPackage(HashSet<CommunicatableEnemyMarker> payload,
                                HumanoidTargeter origin){
        this.payload = new HashSet<CommunicatableEnemyMarker>(payload);
        issuer = origin;
        alreadyCommunicated = new HashSet<HumanoidTargeter>();
        alreadyCommunicated.Add(origin);
        id = counter;
        counter++;
    }

    public CommunicationPackage RecievedBy(HumanoidTargeter newIssuer){
        CommunicationPackage newPackage = new CommunicationPackage(
            new HashSet<CommunicatableEnemyMarker>(this.payload),
            newIssuer
        );
       // Debug.Log("Inside RecievedBy of issuer " + newIssuer.gameObject.name + " "  + newPackage.GetPayload().Count);
        newPackage.alreadyCommunicated = new HashSet<HumanoidTargeter>(
            this.alreadyCommunicated
        );
        newPackage.alreadyCommunicated.Add(newIssuer);
        return newPackage;
    }
    public void AddToCommunicated(HumanoidTargeter targeter){
        alreadyCommunicated.Add(targeter);
    }

    public bool AlreadyCommunicated(HumanoidTargeter targeter){
        return alreadyCommunicated.Contains(targeter);
    }

    public HashSet<CommunicatableEnemyMarker> GetPayload(){
        return new HashSet<CommunicatableEnemyMarker>(payload);
    }

    public HumanoidTargeter GetIssuer(){
        return issuer;
    }
}
