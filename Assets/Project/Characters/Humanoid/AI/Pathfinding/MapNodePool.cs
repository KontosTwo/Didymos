using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodePool : MonoBehaviour {
    [SerializeField]
    private string gameObjectName;

    private Pool<MapNode> pool;

    private static string staticGameObjectName;
    // Use this for initialization

    private void Awake()
    {
        staticGameObjectName = gameObjectName;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static MapNodePool GetInstance(){
        return GameObject.Find(staticGameObjectName).GetComponent<MapNodePool>();
    }
}
