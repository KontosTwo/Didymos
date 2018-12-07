using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class DestinationFinderHelper : MonoBehaviour{
    [SerializeField]
    private Grid grid;

    private static DestinationFinderHelper instance;

    private void Awake(){
        instance = this;
    }

    public static List<MapNode> FindDestinationCandidates(
        Vector3 start,
        IFindCandidates candidateFinder,
        IDestinationFilterer filterer,
        IDestinationCostCalculator costCalculator,
        IDestinationSorter sorter
    ){
        List<MapNode> candidates =
            candidateFinder.FindDestinationCandidates(
                start,
                instance.grid
            );

        List<MapNode> itemsToRemove = candidates.Where(c => !filterer.KeepDestination(c)).ToList();
        foreach (var itemToRemove in itemsToRemove){
            candidates.Remove(itemToRemove);
        }

        SortedDictionary<CostResult, MapNode> sortedCandidates =
            new SortedDictionary<CostResult, MapNode>(
                sorter
            );

        candidates.ForEach(c =>{
            sortedCandidates.Add(costCalculator.GetAdditionalCostAt(c.GetLocation()),c);
        });

        return sortedCandidates.Values.ToList();
    }



}

