using UnityEngine;
using UnityAdditions.Extensions;

public class PositionRandomizer : MonoBehaviour
{
    public Collider Collider { get; set; }

    private void Update()
    {
        Vector3 n = new Vector3();
        n.RandomizeWithin(Collider.bounds);
        transform.position = n;
    }
}
