using System.Collections;
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
    private bool isHopping = false;

    public bool IsHopping { get => isHopping; set => isHopping = value; }

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
        if (IsHopping)
        {
            return;
        }

        var photonView = GetComponent<PhotonView>();
        if (photonView == null)
        {
            Debug.Log("cannot find PhotonView in this gameobject");
        }
        if (!photonView.IsMine) return;
        Vector2 horizontalMovementVector = new Vector2();
        Vector2 verticalMovementVector = new Vector2();

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

    class HoppingParams{
        public Vector3 startPos;
        public Vector3 endPosition;
    }
    public void startHopping(Vector3 startPos, Vector3 endPosition)
    {
        IsHopping = true;
        var param = new HoppingParams();
        param.startPos = startPos;
        param.endPosition = endPosition;
        StartCoroutine("_hopping", param);
    }

    IEnumerator _hopping(HoppingParams param)
    {
        var startPos = param.startPos;
        var endPosition = param.endPosition;
        float timer = 0;
        const float hoppingTime = 1.5f;
        double[] x = new double[3];
        double[] y = new double[3];
        x[0] = 0;
        y[0] = startPos.y;
        x[1] = hoppingTime;
        y[1] = endPosition.y;
        x[2] = 3f / 4f * hoppingTime;
        y[2] = 0;


        var result = Math.MultiLine(x, y, 3, 2);

        Debug.Log(result);
        while (timer < hoppingTime)
        {
            var newX = Mathf.Lerp(startPos.x, endPosition.x, timer/hoppingTime);
            var newY = (float)(result[2] + result[1] * timer + result[0] * Mathf.Pow(timer, 2));
            rb.MovePosition(new Vector2(newX, newY));
            timer += Time.deltaTime;
            yield return null;
        }

        IsHopping = false;
    }



}
