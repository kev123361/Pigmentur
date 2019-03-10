using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int stonesLeft;

    public delegate void LevelComplete();
    public static event LevelComplete OnLevelComplete;
    private void OnEnable()
    {
        Stone.OnStoneFilled += DecreaseStoneCount;
    }

    private void OnDisable()
    {
        Stone.OnStoneFilled -= DecreaseStoneCount;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseStoneCount()
    {
        stonesLeft -= 1;
        if (stonesLeft <= 0)
        {
            if (OnLevelComplete != null) {
                Debug.Log("Called onlevelcomplete");
                OnLevelComplete();
            }
        }
    }
}
