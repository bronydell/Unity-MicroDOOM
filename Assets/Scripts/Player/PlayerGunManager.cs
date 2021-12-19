using System.Collections.Generic;

public class PlayerGunManager
{
    private List<Gun> availableGunList;
    private int currentGunIndex = -1;

    private Gun CurrentGun => currentGunIndex >= 0 && currentGunIndex < availableGunList.Count ? availableGunList[currentGunIndex] : null;
    public PlayerGunManager()
    {
        availableGunList = new List<Gun>();
    }

    public void SetupDependencies(PlayerInput playerInput)
    {
        playerInput.OnFire += OnFire;
        playerInput.OnCycleWeapon += OnGunCycle;
    }

    public void AddGunToPool(Gun gun)
    {
        gun.gameObject.SetActive(false);
        availableGunList.Add(gun);
        if (currentGunIndex == -1)
        {
            SelectGunIndex(0);
        }
    }
    
    private void OnFire(ButtonState buttonState)
    {
        if (buttonState != ButtonState.PressDown) return;
        
        CurrentGun.Shoot();
    }

    private void OnGunCycle(ButtonState buttonState)
    {
        if (buttonState != ButtonState.PressDown) return;

        int nextGunIndex = GetNextGunIndex();
        SelectGunIndex(nextGunIndex);
    }

    private int GetNextGunIndex()
    {
        int nextGunIndex = currentGunIndex + 1;
        return nextGunIndex >= availableGunList.Count ? 0 : nextGunIndex;
    }

    private void SelectGunIndex(int nextGunIndex)
    {
        if (CurrentGun != null)
        {
            CurrentGun.gameObject.SetActive(false);
        }
        currentGunIndex = nextGunIndex;
        CurrentGun.gameObject.SetActive(true);
    }
}