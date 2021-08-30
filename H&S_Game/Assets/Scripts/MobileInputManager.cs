using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    public Joystick joystick;
    public float horizontalMovement; 
    // Start is called before the first frame update
    void Start()
    {
        horizontalMovement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = joystick.Horizontal;
    }
}
