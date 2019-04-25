using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;



public class ButterflyAnimation : MonoBehaviour
{
    public SpriteMeshAnimation[] colors;
    public int delay;
    private int counter;
    private bool looper;

    private void Start()
    {
        looper = true;
        counter = 0;
    }

    public void Update()
    {
        if (counter == delay)
        {
            if (colors[0].frame == 0)
            {
                looper = true;
            }
            else if (colors[0].frame == colors[0].frames.Length - 1)
            {
                looper = false;
            }
            for (int i = 0; i < colors.Length; i++)
            {
                if (looper)
                {
                    Debug.Log(colors[i].frame);
                    colors[i].frame += 1;
                    Debug.Log(colors[i].frame);
                }
                else
                {
                    Debug.Log(colors[i].frame);
                    colors[i].frame -= 1;
                    Debug.Log(colors[i].frame);
                }
            }
            counter = 0;
        }
        else
        {
            counter++;
        }
    }
}
