using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // SERIALIZED PRIVATE VARIABLES
    [SerializeField] private Camera tarCamera;
    [SerializeField] private float smoothSpeed = 0.125f;
    
    // SINGLETON
    static CameraMovement instance;
    public static CameraMovement i
    {
        get
        {
            if(instance == null)
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
        Vector3 delta = new Vector3(-deltaDrag.x, -deltaDrag.y, 0) * Time.deltaTime;
        targetPosition += delta;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
    
}
