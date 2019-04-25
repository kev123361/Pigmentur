using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator canvasAnim;
    // Start is called before the first frame update
    void Start()
    {
        canvasAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFade()
    {
        canvasAnim.SetTrigger("fadeout");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameLvl1");
    }

    public void OpenOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
