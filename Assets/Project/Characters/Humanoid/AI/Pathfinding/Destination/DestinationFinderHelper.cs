using System;
using System.Collections.Generic;
using UnityEngine;
public class DestinationFinderHelper : MonoBehaviour{
    [SerializeField]
    private Grid grid;

    private static DestinationFinderHelper instance;

    private void Awake(){
        instance = this;
    }

    public static SortedDictionary<int,MapNode> FindDestinationCandidates(
        IImplementDestinationFinder implementation,
        IDestinationFilterer filterer,
        IDestinationCostCalculator costCalculator
    ){
        return implementation.FindDestinationCandidates(
            instance.grid,
            filterer,
            costCalculator
        );
    }

}

