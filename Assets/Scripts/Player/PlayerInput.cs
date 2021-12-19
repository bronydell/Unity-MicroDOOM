using UnityEngine;

public enum ButtonState
{
    PressDown,
    Hold,
    PressUp
}

public delegate void AxisHandler(float axisValue);
public delegate void ButtonHandler(ButtonState buttonState);

public class PlayerInput : MonoBehaviour
{
    public event AxisHandler OnHorizontalAxis;
    public event ButtonHandler OnFire;
    public event ButtonHandler OnCycleWeapon;

    private void Update()
    {
        ExecuteButtonHandle("Fire1", OnFire);
        ExecuteButtonHandle("CycleWeapon", OnCycleWeapon);
        OnHorizontalAxis?.Invoke(Input.GetAxis("Horizontal"));
    }

    private void ExecuteButtonHandle(string actionName, ButtonHandler handler)
    {
        if (Input.GetButtonDown(actionName))
        {
            handler?.Invoke(ButtonState.PressDown);
        }
        else if (Input.GetButtonUp(actionName))
        {
            handler?.Invoke(ButtonState.PressUp);
        }
        else if (Input.GetButton(actionName))
        {
            handler?.Invoke(ButtonState.Hold);
        }
    }
}
