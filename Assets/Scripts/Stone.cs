using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private SpriteRenderer sr;

    public bool filled;
    public Player.Color color;
    public Sprite filledSprite;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill()
    {
        filled = true;
        sr.sprite = filledSprite;
    }
}
