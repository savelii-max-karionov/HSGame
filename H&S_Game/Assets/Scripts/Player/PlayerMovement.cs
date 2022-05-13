using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject inputManager;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private static List<float> levels = new List<float> {-3.1f, -2.1f};
    private Vector2 horizontalMovementVector;
    private float horizontalRawAxis;
    private float verticalRawAxis;
    private bool m_FacingRight;
    public int curLevel=0;
    
    // Start is called before the first frame update
    void Start()
    {
        m_FacingRight = false;

    }


    private void FixedUpdate()
    {
        Vector2 horizontalMovementVector = new Vector2();
        Vector2 verticalMovementVector = new Vector2();

        /* GetAxisRaw returns only integer values 
           GetAxis returns real values that change depending on the lenght of the press*/
        if (inputManager.GetComponent<MobileInputManager>() != null)
        {
            horizontalRawAxis = inputManager.GetComponent<MobileInputManager>().horizontalMovement;
        }
        else
        {
            horizontalRawAxis = Input.GetAxisRaw("Horizontal");
        }

        // When left shift is hold down, player will stop moving.
        if ((horizontalRawAxis != 0) && (!Input.GetKey(KeyCode.LeftShift)))
        {
            horizontalMovementVector.x = horizontalRawAxis * movementSpeed * Time.deltaTime;
            horizontalMovementVector.y = 0;
            rb.MovePosition(rb.position + horizontalMovementVector);

            if (horizontalRawAxis > 0 && !m_FacingRight)
            {
                Flip();
            } else if (horizontalRawAxis < 0 && m_FacingRight)
            {
                Flip();
            }
        }


        if (inputManager.GetComponent<MobileInputManager>() != null)
        {
            verticalRawAxis = inputManager.GetComponent<MobileInputManager>().verticalMovement;
            if (verticalRawAxis > 0) verticalRawAxis = 1;
            if (verticalRawAxis < 0) verticalRawAxis = -1;
        }
        else
        {
            verticalRawAxis = Input.GetAxisRaw("Vertical");
        }

        curLevel = (int)Mathf.Clamp(curLevel + verticalRawAxis, 0f, levels.Count - 1f);

        verticalMovementVector.y = levels[curLevel]-rb.position.y;

        rb.MovePosition(rb.position + horizontalMovementVector + verticalMovementVector);
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
