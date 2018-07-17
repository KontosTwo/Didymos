using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommunicationPackage  {
    private HashSet<EnemyMarker> payload;
    private HumanoidTargeter issuer;

    private DateTime issued;
    private HashSet<HumanoidTargeter> alreadyCommunicated;

    public CommunicationPackage(HashSet<EnemyMarker> payload,
                                HumanoidTargeter origin){
        this.payload = payload;
        issuer = origin;
        issued = DateTime.Now;
        alreadyCommunicated = new HashSet<HumanoidTargeter>();
        alreadyCommunicated.Add(origin);
    }

    public void ChangeIssuer(HumanoidTargeter newIssuer){
        issuer = newIssuer;
        alreadyCommunicated.Add(newIssuer);
    }

    public bool IsOutOfDate(DateTime recieverLastUpdated){
        return DateTime.Compare(recieverLastUpdated, issued) < 0;
    }
}
