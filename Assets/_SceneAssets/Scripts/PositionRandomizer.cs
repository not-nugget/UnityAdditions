using UnityEngine;
using UnityAdditions.Old.Vector3;

public class PositionRandomizer : MonoBehaviour
{
    public Collider Collider { get; set; }

    private void Update()
    {
        Vector3 n = new Vector3();
        n.RandomizeInside(Collider.bounds);
        transform.position = n;
    }
}
