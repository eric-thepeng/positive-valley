using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // SERIALIZED PRIVATE VARIABLES
    [SerializeField] private Camera tarCamera;
    [SerializeField] private float smoothSpeed = 5.0f; // Adjust this value to control smoothing speed

    // SINGLETON
    static CameraMovement instance;
    public static CameraMovement i
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraMovement>();
            }
            return instance;
        }
    }

    // PRIVATE VARIABLES
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    public void MoveCamera(Vector2 deltaDrag)
    {
        Vector3 delta = ScreenToWorldDelta(deltaDrag);
        targetPosition += delta;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private Vector3 ScreenToWorldDelta(Vector2 deltaDrag)
    {
        if (tarCamera.orthographic)
        {
            float cameraSize = tarCamera.orthographicSize;
            float aspectRatio = (float)Screen.width / Screen.height;
            return new Vector3(-deltaDrag.x * cameraSize * 2 * aspectRatio, -deltaDrag.y * cameraSize * 2, 0);
        }
        else
        {
            Vector3 delta = new Vector3(-deltaDrag.x, -deltaDrag.y, 0);
            delta = tarCamera.ScreenToWorldPoint(delta) - tarCamera.ScreenToWorldPoint(Vector3.zero);
            return delta;
        }
    }
}