using UnityEngine;
using UnityAdditions.Singleton;

public class RandomizerSpawner : Singleton<RandomizerSpawner>
{
    public GameObject spawn;
    public int count = 100;

    private void Awake()
    {
        for (int i = 0; i < count; i++) Instantiate(spawn, transform).GetComponent<PositionRandomizer>().Collider = GetComponent<Collider>();
    }

}
