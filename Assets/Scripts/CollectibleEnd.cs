using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleEnd : MonoBehaviour
{
    private int collectibles = 0;

    public Text collectibleText;

    // Start is called before the first frame update
    void Start()
    {
        collectibles = 0;
    }

    private void OnEnable()
    {
        JarOpener.OnCollectible += IncrementCollectibles;
        LevelManager.OnLevelComplete += SetCollectibleText;

    }

    private void OnDisable()
    {
        JarOpener.OnCollectible -= IncrementCollectibles;
        LevelManager.OnLevelComplete -= SetCollectibleText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementCollectibles()
    {
        collectibles += 1;
    }

    public void SetCollectibleText()
    {
        collectibleText.text = collectibles.ToString();
    }
}
