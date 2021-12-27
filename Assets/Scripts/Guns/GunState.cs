public class GunState
{
        private bool isPrimed;
        private int currentAmmo;
        private int maxAmmo;

        public event IntChangeHandler OnCurrentAmmoChanged;
        public event IntChangeHandler OnMaxAmmoChanged;

        public int CurrentAmmo
        {
                get => currentAmmo;
                set
                {
                        int oldValue = currentAmmo;
                        currentAmmo = value;
                        OnCurrentAmmoChanged?.Invoke(oldValue, value);
                }
        }

        public int MaxAmmo
        {
                get => maxAmmo;
                set
                {
                        int oldValue = maxAmmo;
                        maxAmmo = value;
                        OnMaxAmmoChanged?.Invoke(oldValue, value);
                }
        }

        public bool IsPrimed
        {
                get => isPrimed;
                set
                {
                        isPrimed = value;
                }
        }

        
        public GunState(int maxAmmo)
        {
                MaxAmmo = maxAmmo;
                isPrimed = false;
                ResetAmmo();
        }

        public void ResetAmmo()
        {
                CurrentAmmo = maxAmmo;
        }
}