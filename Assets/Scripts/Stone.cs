using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private SpriteRenderer sr;

    public bool filled;
    public Player.Color color;
    public Sprite redFilled;
    public Sprite blueFilled;
    public Sprite greenFilled;
    public Sprite yellowFilled;

    public delegate void StoneFilled();
    public static event StoneFilled OnStoneFilled;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(Player.Color color)
    { 
        
        if (!filled && OnStoneFilled != null)
        {
            if (color == Player.Color.Red)
            {
                sr.sprite = redFilled;
            } else if (color == Player.Color.Blue)
            {
                sr.sprite = blueFilled;
            } else if (color == Player.Color.Green)
            {
                sr.sprite = greenFilled;
            } else
            {
                sr.sprite = yellowFilled;
            }
            filled = true;
            OnStoneFilled();
        }
    }
}
