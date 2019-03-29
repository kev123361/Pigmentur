using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Pause : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
}
