using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    public Joystick joystick;
    public float horizontalMovement=0;
    public float verticalMovement=0;

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = joystick.Horizontal;
        verticalMovement = joystick.Vertical;
    }
}
