using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommunicationPackage <T> {
    private HashSet<T> payload;
    private HumanoidTargeter issuer;

    private HashSet<HumanoidTargeter> alreadyCommunicated;

    public CommunicationPackage(HashSet<T> payload,
                                HumanoidTargeter origin){
        this.payload = new HashSet<T>(payload);
        issuer = origin;
        alreadyCommunicated = new HashSet<HumanoidTargeter>();
        alreadyCommunicated.Add(origin);
    }

    public CommunicationPackage<T> RecievedBy(HumanoidTargeter newIssuer,
                                             HashSet<T> newPayload){
        CommunicationPackage<T> newPackage = new CommunicationPackage<T>(
            new HashSet<T>(newPayload),
            newIssuer
        );
        Debug.Log("Inside RecievedBy of issuer " + newIssuer.gameObject.name + " " + newPayload.Count + " "  + newPackage.GetPayload().Count);
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

    public HashSet<T> GetPayload(){
        return new HashSet<T>(payload);
    }

    public HumanoidTargeter GetIssuer(){
        return issuer;
    }
}
