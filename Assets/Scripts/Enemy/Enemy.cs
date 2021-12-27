using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable, IDamageable
{
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private int damage; 
        [SerializeField]
        private int scorePerKill; 
        
        public Action OnDie;

        private bool isThinking;
        private Player targetPlayer;

        private void Awake()
        {
                targetPlayer = FindObjectOfType<Player>();
                isThinking = false;
        }

        private void Start()
        {
                var hitboxes = GetComponentsInChildren<Hitbox>();
                foreach (Hitbox hitbox in hitboxes)
                {
                        hitbox.OnHit += OnHitTrigger;
                }
        }

        private void Update()
        {
                // Do not move forward if you are not thinking!
                if (!isThinking) return;
                
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime );
        }

        public void ReturnToPool()
        {
                gameObject.SetActive(false);
        }

        public void RequestFromPool()
        {
                gameObject.SetActive(true);
        }

        public void StartThinking()
        {
                isThinking = true;
                transform.LookAt(targetPlayer.transform);
        }

        public void StopThinking()
        {
                isThinking = false;
        }

        private void OnHitTrigger(GameObject targetObject)
        {
                if (targetObject != targetPlayer.gameObject) return;

                var damageable = targetObject.GetComponent<IDamageable>();
                damageable?.OnHit(gameObject, damage);

                OnDie?.Invoke();
        }

        public void OnHit(GameObject source, int receivedDamage)
        {
                // Dat nesting is oof...
                Singleton<GameManager>.Instance.Player.PlayerState.Score += scorePerKill;
                OnDie?.Invoke();
        }
}