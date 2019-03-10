using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraChange : MonoBehaviour
{
    public float endSize;
    private Camera cam;

    
    public float timeToCompleteZoom;

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
}
