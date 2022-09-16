using System.Collections.Generic;
using HS;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private static List<float> levels = new List<float> { -3.1f, -2.1f };
    private Vector2 horizontalMovementVector;
    private float horizontalRawAxis;
    private float verticalRawAxis;
    private bool m_FacingRight;
    private bool isClimbing;
    public int curLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_FacingRight = false;
        isClimbing = false;
    }

    private void OnEnable()
    {
        m_FacingRight = false;
        inputManager = FindObjectOfType<InputManager>();
    }

    private void FixedUpdate()
    {
        var photonView = GetComponent<PhotonView>();
        if (photonView == null)
        {
            Debug.Log("cannot find PhotonView in this gameobject");
        }
        if (!photonView.IsMine) return;
        Vector2 horizontalMovementVector = new Vector2();
        Vector2 verticalMovementVector = new Vector2();


        // Disappling flip for animation!
        //if (horizontalRawAxis > 0 && !m_FacingRight)
        //{
        //    Flip();
        //}
        //else if (horizontalRawAxis < 0 && m_FacingRight)
        //{
        //    Flip();
        //}

        horizontalMovementVector = getHorizontalMoveVec(horizontalMovementVector);

        if (!isClimbing)
        {
            verticalMovementVector = getVerticalMoveVec(verticalMovementVector);
        }
        rb.MovePosition(rb.position + horizontalMovementVector + verticalMovementVector);
    }

    private Vector2 getHorizontalMoveVec(Vector2 horizontalMovementVector)
    {
        /* GetAxisRaw returns only integer values 
           GetAxis returns real values that change depending on the lenght of the press*/
        if (inputManager != null)
        {
            horizontalRawAxis = inputManager.horizontal;
        }

        // When left shift is hold, player will stop moving.
        if ((horizontalRawAxis != 0) && (!Input.GetKey(KeyCode.LeftShift)))
        {
            horizontalMovementVector.x = horizontalRawAxis * movementSpeed * Time.deltaTime;
            horizontalMovementVector.y = 0;
            //rb.MovePosition(rb.position + horizontalMovementVector);
        }

        return horizontalMovementVector;
    }

    private Vector2 getVerticalMoveVec(Vector2 verticalMovementVector)
    {
        verticalRawAxis = inputManager.vertical;
        if (verticalRawAxis > 0) verticalRawAxis = 1;
        if (verticalRawAxis < 0) verticalRawAxis = -1;
        //Debug.Log(verticalRawAxis);

        //if (inputManager.GetComponent<MobileInputManager>() != null)
        //{
        //    verticalRawAxis = inputManager.vertical;
        //    if (verticalRawAxis > 0) verticalRawAxis = 1;
        //    if (verticalRawAxis < 0) verticalRawAxis = -1;
        //}
        //else if (inputManager.GetComponent<PCInputManager>() != null)
        //{
        //    verticalRawAxis = inputManager.GetComponent<PCInputManager>().vertical;
        //    if (verticalRawAxis > 0) verticalRawAxis = 1;
        //    if (verticalRawAxis < 0) verticalRawAxis = -1;
        //}

        curLevel = (int)Mathf.Clamp(curLevel + verticalRawAxis, 0f, levels.Count - 1f);

        verticalMovementVector.y = levels[curLevel] - rb.position.y;
        return verticalMovementVector;
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

    public void changeClimbingState(bool climbingState)
    {
        isClimbing = climbingState;
    }
}
