using System;

public static class ImplementationStrategyFactory{

    public static ImplementationStrategy CreateFlankingImplementation(
        CostStrategy nonCoverCostStrategy,
        CostStrategy coverCostStrategy
    ){
        return new FlankingImplementation(
            PathfinderNode.CreateFavorDistanceToTargetNode,
            nonCoverCostStrategy,
            PathfinderNode.CreateFavorStrategyCostNode,
            coverCostStrategy
        );
    }
}

