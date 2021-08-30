using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float heighOffset = 2.2f;
    private Vector3 movementVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = playerTransform.position.x;
        movementVector.y = playerTransform.position.y + heighOffset;
        movementVector.z = -10;
        transform.position = movementVector;
    }
}
