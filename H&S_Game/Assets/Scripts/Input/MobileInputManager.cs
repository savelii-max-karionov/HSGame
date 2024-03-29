using HS;

public class MobileInputManager : InputManager
{
    public Joystick joystick;
    public float horizontalMovement = 0;
    public float verticalMovement = 0;


    public override float horizontal
    {
        get { return horizontalMovement; }
    }

    public override float vertical
    {
        get { return verticalMovement; }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = joystick.Horizontal;
        verticalMovement = joystick.Vertical;
    }
}
