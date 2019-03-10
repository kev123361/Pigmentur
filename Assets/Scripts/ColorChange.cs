using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEditor;

public class ColorChange : MonoBehaviour
{
    private Renderer playerRenderer;

    public GameObject[] colorMeshes;
    // Start is called before the first frame update
    public AudioClip wind;
    //public AudioSource audio;
    
    void Start()
    {
        //playerRenderer = GetComponent<Renderer>();
        //AudioSource audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBlue()
    {
        colorMeshes[1].SetActive(true);
    }

    public void ChangeRed()
    {
        colorMeshes[2].SetActive(true);
    }

    public void ChangeYellow()
    {
        colorMeshes[3].SetActive(true);
    }

    public void ChangeGreen()
    {
        colorMeshes[4].SetActive(true);
    }

    public void ChangeWhite()
    {
        colorMeshes[0].SetActive(true);
    }

    public void ChangeColor(Player.Color color)
    {
        switch (color)
        {
            case Player.Color.White:
                colorMeshes[0].SetActive(true);
                break;
            case Player.Color.Blue:
                colorMeshes[1].SetActive(true);
                AudioSource wind = GetComponent<AudioSource>();
                wind.Play();
                //AudioClip.PlayOneShot (wind, 0.0F);
                break;
            case Player.Color.Red:
                colorMeshes[2].SetActive(true);
                break;
            case Player.Color.Yellow:
                colorMeshes[3].SetActive(true);
                break;
            case Player.Color.Green:
                colorMeshes[4].SetActive(true);
                break;
        }
    }

    public void TurnOffMesh(Player.Color color)
    {
        switch (color)
        {
            case Player.Color.White:
                colorMeshes[0].SetActive(false);
                break;
            case Player.Color.Blue:
                colorMeshes[1].SetActive(false);
                break;
            case Player.Color.Red:
                colorMeshes[2].SetActive(false);
                break;
            case Player.Color.Yellow:
                colorMeshes[3].SetActive(false);
                break;
            case Player.Color.Green:
                colorMeshes[4].SetActive(false);
                break;
        }
    }

}
