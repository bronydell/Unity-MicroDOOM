using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
        [SerializeField]
        private int startingHealth = 100;
        [SerializeField]
        private PlayerInput input;
        [SerializeField]
        private PlayerMovement movement;

        private PlayerGunManager gunManager;
        private PlayerState playerState;

        public PlayerState PlayerState => playerState;
        public PlayerGunManager GunManager => gunManager;
        
        private void Awake()
        {
                playerState = new PlayerState(startingHealth);
                gunManager = new PlayerGunManager();
        }

        private void Start()
        {
                gunManager.SetupDependencies(input);
                movement.SetupDependencies(input);
                var starterGuns = GetComponentsInChildren<Gun>(true);
                foreach (var gun in starterGuns)
                {
                        gunManager.AddGunToPool(gun);
                }
                var uiManager = Singleton<UIManager>.Instance;
                uiManager.SetupPlayer(this);
        }

        public void Activate()
        {
                input.EnableInput();
        }

        public void Deactivate()
        {
                input.DisableInput();
        }

        public void OnHit(GameObject source, int receivedDamage)
        {
                playerState.Health -= receivedDamage;
        }
}