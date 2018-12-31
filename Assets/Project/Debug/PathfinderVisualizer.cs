using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PathfinderVisualizer : MonoBehaviour
{
    private List<PathfinderNode> visitedNodes;

    private List<PathfinderNode> nodesToDraw;
    private static PathfinderVisualizer instance;

    private void Awake(){
        visitedNodes = new List<PathfinderNode>();
        nodesToDraw = new List<PathfinderNode>();
        instance = this;
    }

    public static void Visit(PathfinderNode node) {

        instance.visitedNodes.Add(node);
    }

    public static void Visualize() {
        instance.StartCoroutine(CreateVisualizer());
    }

    private static IEnumerator CreateVisualizer() {
        foreach(PathfinderNode node in instance.visitedNodes) {
            instance.nodesToDraw.Add(node);
            yield return new WaitForSeconds(.0010f);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (PathfinderNode node in nodesToDraw)
        {
            Gizmos.color = Color.gray;

            Gizmos.DrawCube(node.GetLocation(), new Vector3(.3f, .3f, .3f));
            //Handles.Label(gizmo.location, gizmo.text);



        }

    }
}
