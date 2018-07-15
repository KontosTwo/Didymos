using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Environment{
	public class MapBound : MonoBehaviour {
        [SerializeField]
        private Collider bounds;
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			//print (GetBottomLeftCorner ());
		}

		void OnDrawGizmos() {
            Gizmos.DrawWireCube(bounds.bounds.center,GetDimensions());
		}

		public Vector3 GetDimensions(){
			return bounds.bounds.extents * 2;
		}

		public Vector3 GetBottomLeftCorner(){
			return bounds.bounds.center - bounds.bounds.extents;
		}
	}
}