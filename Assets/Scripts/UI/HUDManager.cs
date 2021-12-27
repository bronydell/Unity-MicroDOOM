using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextPanel healthPanel;
    [SerializeField]
    private TextPanel ammoPanel;

    // That's a workaround, too tired to do something nice
    private int currentWeaponMaxAmmo = -1;
    
    public void SetupPlayer(Player player)
    {
        OnHealthChanged(-1, player.PlayerState.Health);
        player.PlayerState.OnHealthChanged += OnHealthChanged;
        OnGunChanged(null, player.GunManager.CurrentGun);
        player.GunManager.OnGunChanged += OnGunChanged;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnGunChanged(Gun oldGun, Gun newGun)
    {
        if (oldGun != null)
        {
            oldGun.State.OnCurrentAmmoChanged -= OnAmmoCountChanged;
        }
        newGun.State.OnCurrentAmmoChanged += OnAmmoCountChanged;
        currentWeaponMaxAmmo = newGun.State.MaxAmmo;
        OnAmmoCountChanged(-1, newGun.State.CurrentAmmo);
    }

    private void OnAmmoCountChanged(int oldValue, int newValue)
    {
        ammoPanel.Text = $"{newValue}/{currentWeaponMaxAmmo}";
    }

    private void OnHealthChanged(int oldValue, int newValue)
    {
        healthPanel.Text = $"{newValue}";
    }
}