using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    public AudioClip song2;
    public AudioClip creditsSong;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<Music>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckForNewSong;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckForNewSong;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckForNewSong(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameLvl4")
        {
            audioSource.clip = song2;
            audioSource.Play();
        }
        if (scene.name == "End Credits")
        {
            audioSource.clip = creditsSong;
            audioSource.Play();
        }
    }
}
