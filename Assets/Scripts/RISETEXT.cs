using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RISETEXT : MonoBehaviour
{
    public float speed;
    private RectTransform rect;
    // Update is called once per frame
    private void Start()
    {
        rect = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition3D += new Vector3(0, speed, 0) * Time.deltaTime;
    }
}
