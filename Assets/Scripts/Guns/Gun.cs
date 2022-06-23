using UnityEngine;

public class Gun : MonoBehaviour
{
        [SerializeField]
        protected int maxAmmo;
        [SerializeField]
        protected float cooldown;
        [SerializeField]
        protected Shooter shooter;
        [SerializeField]
        protected Reloader reloader;

        public GunState State { get; private set; }

        private void Awake()
        {
                State = new GunState(maxAmmo);
        }

        private void Start()
        {
                reloader.Setup(this);
                shooter.Setup(this);
        }

        private void Update()
        {
                State.Cooldown = Mathf.Max(State.Cooldown - Time.deltaTime, 0.0f);
        }
        
        public virtual void Shoot()
        {
                // TODO: It would be cool to pass trigger state, so that we can "charge the gun" if needed
                if (reloader.IsReloading()) return;
                if (State.Cooldown > 0.0f) return;

                State.Cooldown = cooldown;
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