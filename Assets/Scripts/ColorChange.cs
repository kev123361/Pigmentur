using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEditor;

public class ColorChange : MonoBehaviour
{
    private Renderer playerRenderer;

    public Material blue;
    public Material red;
    public Material yellow;
    public Material green;

    public GameObject[] colorMeshes;
    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
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
        colorMeshes[2].SetActive(true);
    }

    public void ChangeYellow()
    {
        playerRenderer.material = yellow;
        colorMeshes[3].SetActive(true);
    }

    public void ChangeGreen()
    {
        playerRenderer.material = green;
        colorMeshes[4].SetActive(true);
    }
}
