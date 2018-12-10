using System;
using System.Collections.Generic;
using UnityEngine;

public static class HumanoidAttackPlannerCommunications{
    private static List<HumanoidAttackPlanner> planners;

    static HumanoidAttackPlannerCommunications(){
        planners = new List<HumanoidAttackPlanner>();
    }

    public static void AddPlanner(HumanoidAttackPlanner planner){
        planners.Add(planner);
    }
}

