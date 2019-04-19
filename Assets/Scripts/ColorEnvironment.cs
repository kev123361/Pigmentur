using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEnvironment : MonoBehaviour
{
    SpriteRenderer sprite;
    public float timer = 0f;
    public float timeToComplete = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeColor()
    {
        StartCoroutine(FadeColorIn());
    }

    private IEnumerator FadeColorIn()
    {

        while (timer < timeToComplete)
        {
            sprite.material.SetFloat("_EffectAmount",
                Mathf.Lerp(1f, 0f, timer / timeToComplete));
            timer += Time.deltaTime;
            yield return null;
        }

    }
}
