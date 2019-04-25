using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarOpener : MonoBehaviour
{
    public Animator anim;
    public GameObject butterfly;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("Open Sesame");
    }

    public void ActivateButterfly()
    {
        butterfly.SetActive(true);
    }
}
