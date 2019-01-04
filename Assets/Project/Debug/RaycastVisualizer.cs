using System;
using UnityEngine;
using System.Collections;
public class RaycastVisualizer : MonoBehaviour{
    public Transform start;
    public Transform end;
    public void Start()
    {
        StartCoroutine(DelayedRaycast());
    }
    private void Update()
    {
    }

    private IEnumerator DelayedRaycast()
    {
        while (true)
        {
            //Debug.Log(EnvironmentPhysics.LineOfSightToVantagePointExists(1, start.position, end.position));
            yield return new WaitForSeconds(.2f);
        }

    }
}

