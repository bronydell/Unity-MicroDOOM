using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable, IDamageable
{
        [SerializeField]
        private float movementSpeed; 
        [SerializeField]
        private Hitbox hitbox; 
        
        public Action OnDie; 

        private Player targetPlayer;

        private void Awake()
        {
                targetPlayer = FindObjectOfType<Player>();
        }

        private void Start()
        {
                hitbox.OnHit += OnHitTrigger;
        }

        private void Update()
        {
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
                transform.LookAt(targetPlayer.transform);
        }

        private void OnHitTrigger(GameObject targetObject)
        {
                if (targetObject != targetPlayer.gameObject) return;

                var damageable = targetObject.GetComponent<IDamageable>();
                damageable?.OnHit(gameObject);

                OnDie.Invoke();
        }

        public void OnHit(GameObject source)
        {
                OnDie?.Invoke();
        }
}