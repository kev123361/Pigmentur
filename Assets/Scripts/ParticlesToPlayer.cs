using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesToPlayer : MonoBehaviour
{
    public GameObject player;

    private float timer = 0f;
    public float timeToReachPlayer;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveToPlayer()
    {
        while (timer < timeToReachPlayer)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, timeToReachPlayer - timer);

            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
