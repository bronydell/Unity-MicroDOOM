using System.Collections.Generic;

public delegate void GunChangeHandler(Gun oldGun, Gun newGun);

public class PlayerGunManager
{
    private List<Gun> availableGunList;
    private int currentGunIndex = -1;

    public Gun CurrentGun => currentGunIndex >= 0 && currentGunIndex < availableGunList.Count ? availableGunList[currentGunIndex] : null;

    public event GunChangeHandler OnGunChanged;

    public PlayerGunManager()
    {
        availableGunList = new List<Gun>();
    }

    public void SetupDependencies(PlayerInput playerInput)
    {
        playerInput.OnFire += OnFire;
        playerInput.OnReload += OnReload;
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

    private void OnReload(ButtonState buttonState)
    {
        if (buttonState != ButtonState.PressDown) return;
        
        CurrentGun.Reload();
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
        Gun oldGun = CurrentGun;
        if (oldGun != null)
        {
            oldGun.gameObject.SetActive(false);
            oldGun.Conceal();
        }
        
        currentGunIndex = nextGunIndex;
        
        Gun currentGun = CurrentGun;
        currentGun.gameObject.SetActive(true);
        currentGun.Prime();
    
        OnGunChanged?.Invoke(oldGun, currentGun);
    }
}