using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmFollow : MonoBehaviour
{
    public GameObject player;
    public bool left;

    private BoxCollider2D playerRB;
    private Vector3 offsets;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<BoxCollider2D>();
        offsets = new Vector3(Random.Range(-1.5f, -0.5f), Random.Range(-.5f, 1f), 0f);
        if (left)
        {
            offsets.x = -offsets.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,1) < .15)
        {
            offsets = offsets + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f);
        }
        transform.position = Vector3.Lerp(transform.position, playerRB.transform.position + offsets, Time.deltaTime);
    }
}
