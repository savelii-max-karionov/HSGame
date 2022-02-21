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
    private bool m_FacingRight;
    // Start is called before the first frame update
    void Start()
    {
        m_FacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /* GetAxisRaw returns only integer values 
           GetAxis returns real values that change depending on the lenght of the press*/
        if (inputManager.GetComponent<MobileInputManager>() != null)
        {
            rawAxis = inputManager.GetComponent<MobileInputManager>().horizontalMovement;
        }
        else
        {
            rawAxis = Input.GetAxisRaw("Horizontal");
        }

        if (rawAxis != 0)
        {
            movementVector.x = rawAxis * movementSpeed * Time.deltaTime;
            movementVector.y = 0;
            rb.MovePosition(rb.position + movementVector);

            if (rawAxis > 0 && !m_FacingRight)
            {
                Flip();
            } else if (rawAxis < 0 && m_FacingRight)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
