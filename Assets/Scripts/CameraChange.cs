using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraChange : MonoBehaviour
{
    public float endSize;
    private Camera cam;

    
    public float timeToCompleteZoom;

    public bool zooming;
    public float zoomTargetSize;

    private void OnEnable()
    {
        LevelManager.OnLevelComplete += ZoomOutToEnd;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelComplete -= ZoomOutToEnd;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zooming)
        {
            cam.orthographicSize = Mathf.LerpUnclamped(cam.orthographicSize, zoomTargetSize, Time.deltaTime);
            if (Mathf.Abs(zoomTargetSize - cam.orthographicSize) < 0.1f)
            {
                cam.orthographicSize = zoomTargetSize;
                zooming = false;
            }
        }
    }

    public void ZoomOutToEnd()
    {
        StartCoroutine(CameraZoom());
    }

    private IEnumerator CameraZoom()
    {
        float timer = 0f;
        
        while (timer < timeToCompleteZoom)
        {
            timer += Time.deltaTime;
            cam.orthographicSize = Mathf.LerpUnclamped(cam.orthographicSize, endSize, Time.deltaTime);
            yield return null;
        }
    }

    public void ZoomCameraTo(float size)
    {
        zoomTargetSize = size;
        zooming = true;
    }
}
