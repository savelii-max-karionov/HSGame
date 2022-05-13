using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float heightOffset = 0f;
    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRestoreSpeed = 5f;
    private Vector3 movementVector;
    bool isRestoring=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 movVec = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += movVec * Time.deltaTime * cameraMoveSpeed;
            isRestoring = true;
        }
        // Restore the camera to the player's position.
        else if (isRestoring)
        {
            Vector3 movDir = Vector3.Normalize((playerTransform.position.x - transform.position.x) * Vector3.right);
            // Prevent the camera move too far from the original character position.
            Vector3 deltaPos = Mathf.Min(Mathf.Abs(playerTransform.position.x - transform.position.x),Time.deltaTime * cameraRestoreSpeed) * movDir;
            transform.position += deltaPos;
            if (transform.position == playerTransform.position) isRestoring = false;
        }
        else
        {
            movementVector.x = playerTransform.position.x;
            movementVector.y = heightOffset;
            movementVector.z = -10;
            transform.position = movementVector;
        }
    }
}
