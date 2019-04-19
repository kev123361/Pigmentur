using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnColorOn : MonoBehaviour
{
    public float timer;
    public float timeToComplete;

    public GameObject[] holders;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ColorWorld()
    {
       foreach (GameObject holder in holders)
        {
            foreach (ColorEnvironment terrain in holder.GetComponentsInChildren<ColorEnvironment>())
            {
                terrain.FadeColor();
            }
        }

    }

}
