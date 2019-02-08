using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer playerRenderer;

    public Material blue;
    public Material red;
    public Material yellow;
    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBlue()
    {
        playerRenderer.material = blue;
    }

    public void ChangeRed()
    {
        playerRenderer.material = red;
    }

    public void ChangeYellow()
    {
        playerRenderer.material = yellow;
    }
}
