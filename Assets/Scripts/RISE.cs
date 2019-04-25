using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RISE : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0f, speed, 0f) * Time.deltaTime;
    }
}
