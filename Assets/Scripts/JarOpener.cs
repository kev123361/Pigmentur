using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarOpener : MonoBehaviour
{
    public Animator anim;
    public GameObject butterfly;

    public delegate void Collectible();
    public static event Collectible OnCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollectible.Invoke();
        anim.SetTrigger("Open Sesame");
    }

    public void ActivateButterfly()
    {
        butterfly.SetActive(true);
    }
}
