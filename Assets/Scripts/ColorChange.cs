using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEditor;

public class ColorChange : MonoBehaviour
{
    private Renderer playerRenderer;

    public Material white;
    public Material blue;
    public Material red;
    public Material yellow;
    public Material green;

    public GameObject[] colorMeshes;
    // Start is called before the first frame update
    void Start()
    {
        //playerRenderer = GetComponent<Renderer>();
        colorMeshes[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBlue()
    {
        playerRenderer.material = blue;
        colorMeshes[1].SetActive(true);
    }

    public void ChangeRed()
    {
        playerRenderer.material = red;
        colorMeshes[1].SetActive(true);
    }

    public void ChangeYellow()
    {
        playerRenderer.material = yellow;
        colorMeshes[1].SetActive(true);
    }

    public void ChangeGreen()
    {
        playerRenderer.material = green;
        colorMeshes[1].SetActive(true);
    }

    public void ChangeWhite()
    {
        playerRenderer.material = white;
    }

    public void ChangeColor(Player.Color color)
    {
        switch (color)
        {
            case Player.Color.White:
                colorMeshes[0].SetActive(true);
                break;
            case Player.Color.Red:
                colorMeshes[1].SetActive(true);
                break;
            case Player.Color.Blue:

                colorMeshes[1].SetActive(true);
                break;
            case Player.Color.Yellow:
                colorMeshes[1].SetActive(true);
                break;
            case Player.Color.Green:
                colorMeshes[1].SetActive(true);
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
            case Player.Color.Red:
                colorMeshes[1].SetActive(false);
                break;
            case Player.Color.Blue:

                colorMeshes[1].SetActive(false);
                break;
            case Player.Color.Yellow:
                colorMeshes[1].SetActive(false);
                break;
            case Player.Color.Green:
                colorMeshes[1].SetActive(false);
                break;
        }
    }

}
