using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAdditions.Colors;

[ExecuteAlways]
public class ColorsTest : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Colors.Byzantium;
        Gizmos.DrawWireSphere(transform.position, 10);
        Gizmos.color = Colors.BurntOrange;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 5);
        Gizmos.color = Colors.BlanchedAlmond;
    }
}
