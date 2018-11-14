using System;

using UnityEngine;

public class PathfinderStrategyFactory : MonoBehaviour
{
    private static PathfinderStrategyFactory instance;
    [SerializeField]
    private Grid grid;

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




	public static PathfinderStrategy CreateNoStrategy(){
		return new NoStrategy();
	}

	public static PathfinderStrategy CreateScootStrategy(Vector3 targetVantage){
		return new ScootStrategy(instance.grid,targetVantage);
	}

	private class NoStrategy : PathfinderStrategy{
        public int GetCostAt(Point start, Point end){
			return 0;
		}
	}

	private class ScootStrategy : PathfinderStrategy{
        private Grid grid;
        private Vector2 vantagePoint;

        public ScootStrategy(Grid grid,Vector3 targetVantage){
            this.grid = grid;
		}

		public int GetCostAt(Point start,Point end){
			return 0;
		}
	}
}




