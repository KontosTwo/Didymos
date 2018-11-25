using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlankStrategy : MonoBehaviour, PathfinderStrategy
{
    [SerializeField]
    private float goingUpPenalty = 1f;
    [SerializeField]
    private float goingDownPenalty = 1f;
    [SerializeField]
    private float climbUpPenalty = 1f;
    [SerializeField]
    private float climbDownPenalty = 1f;
    [SerializeField]
    private float climbUpThreshold = 1f;
    [SerializeField]
    private float climbDownThreshold = 1f;

    [SerializeField]
    private float coverDisparityPenalty = 1f;
    [SerializeField]
    private float negligibleCoverThreshold = 0.1f;

    private Grid grid;
    private HumanoidVantage strategizer;
    private List<HumanoidVantage> enemies;



    public FlankStrategy(Grid grid,
                         HumanoidVantage s,
                         List<HumanoidVantage> e)
    {
        this.grid = grid;
        this.strategizer = s;
        this.enemies = e;
    }
    /*
     * Need to cache the result of this
     * Also wrap chunks of the logic in static functions
     */
    public int GetAdditionalCostAt(Point start, Point end)
    {
        Vector3 startVector = grid.NodeToWorldCoord(start);
        Vector3 endVector = grid.NodeToWorldCoord(end);

        float heightDifference = endVector.y - startVector.y;
        float heightPenalty = CostCalculator.CalculateHeightPenalty(
            heightDifference,
            goingUpPenalty,
            climbUpThreshold,
            climbUpThreshold,
            goingDownPenalty,
            climbDownPenalty,
            climbDownThreshold
        );

        float totalCoverDisparity = CostCalculator.CalculateTotalCoverDisparity(
            strategizer,
            enemies,
            endVector,
            coverDisparityPenalty,
            negligibleCoverThreshold
        );

        return (int)(heightPenalty + totalCoverDisparity);
    }
}