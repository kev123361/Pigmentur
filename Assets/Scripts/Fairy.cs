using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    public Player.Color currColor;
    // Start is called before the first frame update
    void Start()
    {
        currColor = Player.Color.White;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player.Color GetColor()
    {
        return currColor;
    }

    public void SetColor(Player.Color color)
    {
        currColor = color;
    }
}
