using System;
using System.Collections.Generic;
using UnityEngine;
public class FlankEnemyImplementation : DestinationImplementationStrategy{
    [SerializeField]
    private HumanoidTargeter targeter;

    public FlankEnemyImplementation(){

    }




    public override SortedDictionary<int, MapNode> FindDestinationCandidates(
        Grid grid,
        IDestinationFilterer filterer,
        IDestinationCostCalculator costCalculator
    ){
        SortedDictionary<int, MapNode> candidates =
            new SortedDictionary<int, MapNode>(
                new SortByCost()
            );


    }
}

