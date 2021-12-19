using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
        [SerializeField]
        private PlayerInput input;
        [SerializeField]
        private PlayerMovement movement;

        private PlayerGunManager gunManager;

        private void Awake()
        {
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
        }

        public void OnHit(GameObject source)
        {
                // Debug.Log("Player is dead!");
                SceneManager.LoadScene(0);
        }
}