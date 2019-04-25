using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleEnd : MonoBehaviour
{
    private int collectibles = 0;

    // Start is called before the first frame update
    void Start()
    {
        collectibles = 0;
    }

    private void OnEnable()
    {
        JarOpener.OnCollectible += IncrementCollectibles;
    }

    private void OnDisable()
    {
        JarOpener.OnCollectible -= IncrementCollectibles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementCollectibles()
    {
        collectibles += 1;
    }
}
