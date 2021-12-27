using UnityEngine;

public enum ButtonState
{
    PressDown,
    Hold,
    PressUp
}

public delegate void AxisHandler(float axisValue);
public delegate void MouseAxisHandler(Vector2 mouseDeltaValue);
public delegate void ButtonHandler(ButtonState buttonState);

public class PlayerInput : MonoBehaviour
{
    public event AxisHandler OnHorizontalAxis;
    public event MouseAxisHandler OnMouseHorizontal;
    public event AxisHandler OnVerticalAxis;
    public event ButtonHandler OnFire;
    public event ButtonHandler OnReload;
    public event ButtonHandler OnCycleWeapon;

    private bool isInputActive;

    public void EnableInput()
    {
        isInputActive = true;
    }

    public void DisableInput()
    {
        isInputActive = false;
    }

    private void Update()
    {
        if (!isInputActive) return;

        ExecuteButtonHandle("Fire1", OnFire);
        ExecuteButtonHandle("Reload", OnReload);
        ExecuteButtonHandle("CycleWeapon", OnCycleWeapon);
        OnHorizontalAxis?.Invoke(Input.GetAxis("Horizontal"));
        OnVerticalAxis?.Invoke(Input.GetAxis("Vertical"));
        OnMouseHorizontal?.Invoke(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
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
