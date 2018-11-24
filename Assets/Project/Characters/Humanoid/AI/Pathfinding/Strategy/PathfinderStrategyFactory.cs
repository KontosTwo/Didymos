using System;

using UnityEngine;

public abstract class PathfinderStrategyFactory : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    private static PathfinderStrategyFactory instance;

    public PathfinderStrategyFactory()
    {

    }

    void Awake()
    {
        instance = this;
        instance.grid = grid;
    }

    void Start()
    {

    }





	/*public static PathfinderStrategy CreateFlankStrategy(){
        return new FlankStrategy(instance.grid,targetVantage);
	}*/


}




