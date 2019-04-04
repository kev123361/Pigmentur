using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int stonesLeft;
    
    public Object[] scenes;

    public delegate void LevelComplete();
    public static event LevelComplete OnLevelComplete;
    private void OnEnable()
    {
        Stone.OnStoneFilled += DecreaseStoneCount;
        OnLevelComplete += LoadNextLevel;

    }

    private void OnDisable()
    {
        Stone.OnStoneFilled -= DecreaseStoneCount;
        OnLevelComplete -= LoadNextLevel;
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

    public void LoadNextLevel()
    {
        Scene currScene = SceneManager.GetActiveScene();
        int newSceneIndex = -1;
        for (int i = 0; i < scenes.Length - 1; i++)
        {
            if (scenes[i].name == currScene.name)
            {
                newSceneIndex = i + 1;
            }
        }

        SceneManager.LoadScene(scenes[newSceneIndex].name);
     }
}
