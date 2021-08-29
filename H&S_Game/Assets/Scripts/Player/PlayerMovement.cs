using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject inputManager;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movementVector;
    private float rawAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /* GetAxisRaw returns only integer values 
           GetAxis returns real values that change depending on the lenght of the press*/
        rawAxis = Input.GetAxisRaw("Horizontal");
        if (rawAxis != 0)
        {
            movementVector.x = rawAxis * movementSpeed * Time.deltaTime;
            movementVector.y = 0;
            rb.MovePosition(rb.position + movementVector);
        }
    }
}
