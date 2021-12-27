using UnityEngine;

public class Gun : MonoBehaviour
{
        [SerializeField]
        protected int maxAmmo;
        [SerializeField]
        protected Shooter shooter;
        [SerializeField]
        protected Reloader reloader;

        public GunState State { get; private set; }
        public Shooter Shooter => shooter;

        private void Awake()
        {
                State = new GunState(maxAmmo);
        }

        private void Start()
        {
                reloader.Setup(this);
                shooter.Setup(this);
        }

        public virtual void Shoot()
        {
                // TODO: It would be cool to pass trigger state, so that we can "charge the gun" if needed
                if (reloader.IsReloading()) return;
                shooter.Shoot();
        }

        public virtual void Reload()
        {
                reloader.StartReloading();
        }


        public virtual void Prime()
        {
                State.IsPrimed = true;
        }

        public virtual void Conceal()
        {
                State.IsPrimed = false;
                reloader.CancelReloading();
        }
}