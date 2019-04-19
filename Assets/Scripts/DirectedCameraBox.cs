using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedCameraBox : MonoBehaviour
{
    public Camera mainCamera;

    public bool isCameraFollowing;
    public float viewportSize;
    public float lookAheadX;

    //Default values to return to
    public float defaultViewportSize = 7f;
    public float defaultLookAheadX = 1.7f;

    //private bool newSize;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();

        //Grab default values
        defaultViewportSize = mainCamera.orthographicSize;
        defaultLookAheadX = mainCamera.GetComponent<CameraFollow>().lookAheadDstX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.GetComponent<CameraFollow>().enabled = isCameraFollowing;
            mainCamera.GetComponent<CameraChange>().ZoomCameraTo(viewportSize);
            mainCamera.GetComponent<CameraFollow>().lookAheadDstX = lookAheadX;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.GetComponent<CameraFollow>().enabled = true;
            mainCamera.GetComponent<CameraChange>().ZoomCameraTo(defaultViewportSize);
            mainCamera.GetComponent<CameraFollow>().lookAheadDstX = defaultLookAheadX;
        }
    }
}
