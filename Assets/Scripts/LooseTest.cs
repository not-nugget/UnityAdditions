using UnityEngine;
using UnityAdditions.LooseBounds;
using UnityAdditions.LooseRect;
using UnityAdditions.Old.Vector3;
using UnityAdditions.Old.Vector2;

public class LooseTest : MonoBehaviour
{
    public LooseBounds lb = null;
    public LooseRect lr = null;

    public float updateScale = .5f;

    public GameObject debugPrefab = null;

    GameObject[] spawned;
    float timer = 0;

    private void Start()
    {
        spawned = new GameObject[]
        {
            Instantiate(debugPrefab, transform),
            Instantiate(debugPrefab, transform)
        };
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector3 point3 = new Vector3();
            //point3.RandomizeValues(lb.bounds.size * 2 - lb.bounds.center);
            point3.RandomizeInside(lb);
            print($"the point {point3} is inside the bounds: {lb[point3]}");
            spawned[0].transform.position = point3;

            Vector2 point2 = new Vector2();
            //point2.RandomizeValues(lr.rect.size * 2 - lr.rect.center);
            point2.RandomizeInside(lr);
            print($"the point {point2} is inside the rect: {lr[point2]}");
            spawned[1].transform.position = point2;

            timer = updateScale;
        }
    }
}