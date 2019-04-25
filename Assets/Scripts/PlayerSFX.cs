using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioClip[] footfallSFX;
    public AudioClip jumpSFX;
    public AudioClip dashSFX;
    public AudioClip absorbSFX;
    public AudioClip insertSFX;

    [SerializeField] private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
       
        audio.volume = .367f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootfall()
    {
        audio.pitch = Random.Range(.8f, 1.2f);
        audio.PlayOneShot(footfallSFX[Random.Range(0, footfallSFX.Length)], Random.Range( .3f, .35f));
    }

    public void PlayJump()
    {
        audio.pitch = Random.Range(.8f, 1.2f);
        audio.PlayOneShot(jumpSFX);
    }

    public void PlayDash()
    {
        audio.pitch = Random.Range(.8f, 1.2f);
        audio.PlayOneShot(dashSFX, .3f);
    }

    public void PlayAbsorb()
    {
        audio.pitch = Random.Range(.8f, 1.2f);
        audio.PlayOneShot(absorbSFX);
    }

    public void PlayInsert()
    {
        audio.pitch = Random.Range(.8f, 1.2f);
        audio.PlayOneShot(insertSFX);
    }
}
