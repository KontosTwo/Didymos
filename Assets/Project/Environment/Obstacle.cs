using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine;

public class Obstacle : MonoBehaviour,Projectile.IObstructable {
	// Editor variables


	// Editor variables
	[SerializeField]
	[Range(0,100)]
	private int resistance;
	[SerializeField]
	[Range(0,100)]
	private int opacity;
	private MeshCollider col;
	[SerializeField]
	private Damage onHitByProjectile;
	[SerializeField]
	private bool isWalkable;
    /* 
     * Obstacles such as bushes, smoke clouds, etc
     * that provide a visual obstacle, but do not factor
     * into physical height calculations
     */
	[SerializeField]
	private bool canPhaseThrough;


	void Awake(){	
		col = GetComponent<MeshCollider> ();
	}

	public int GetResistance(){
		return resistance;
	}

	public int GetTransparency(){
		return opacity;
	}

	public void HitBy(Projectile projectile){
		onHitByProjectile.Invoke (projectile);
	}

	public bool IsWalkable(){
		return isWalkable;
	}

	public bool CanPhaseThrough(){
		return canPhaseThrough;
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	[Serializable]
	public class Damage : UnityEvent<Projectile>{

	}
}
