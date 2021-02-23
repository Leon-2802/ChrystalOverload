using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class bakeNavMesh : MonoBehaviour
{
    public levelGeneration getBool;
    void Update()
    {
        if(getBool.stopGeneration == true )
        {
            StartCoroutine(buildNav());
        }
    }

    private IEnumerator buildNav()
    {
        yield return new WaitForSeconds(1f);
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }
}
