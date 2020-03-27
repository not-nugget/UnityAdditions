using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerSpawner : MonoBehaviour
{
    public GameObject spawn;
    public int count = 100;

    private void Awake()
    {
        for (int i = 0; i < count; i++) Instantiate(spawn, transform).GetComponent<PositionRandomizer>().Collider = GetComponent<Collider>();
    }

}
