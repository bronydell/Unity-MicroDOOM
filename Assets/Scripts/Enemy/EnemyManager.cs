using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
        [SerializeField]
        private float spawnDelay;
        [SerializeField]
        private float spawnRadius;
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private Transform baseSpawnPoint;

        private ObjectPool<Enemy> enemyPool;

        private Coroutine spawnCoroutine;

        private void Awake()
        {
                enemyPool = new ObjectPool<Enemy>(SpawnEnemy);
        }

        private void Start()
        {
                StartSpawning();
        }

        public void StartSpawning()
        {
                spawnCoroutine = StartCoroutine(SpawnerCoroutine());
        }

        public void StopSpawning()
        {
                if (spawnCoroutine != null)
                {
                        StopCoroutine(spawnCoroutine);
                }
        }
        
        private Enemy SpawnEnemy()
        {
                return Instantiate(enemyPrefab).GetComponent<Enemy>();
        }

        private IEnumerator SpawnerCoroutine()
        {
                while (true)
                {
                        var enemy = enemyPool.GetObjectFromPool();
                        enemy.OnDie = () =>
                        {
                                enemyPool.ReturnObjectToPool(enemy);
                        };
                        enemy.transform.position = GetRandomPosition();
                        enemy.StartThinking();
                        yield return new WaitForSeconds(spawnDelay);
                }
        }

        private Vector3 GetRandomPosition()
        {
                Vector3 spawnPoint = baseSpawnPoint.position;
                Vector2 randomPointOnCircle = Random.insideUnitCircle * spawnRadius;
                spawnPoint.x = randomPointOnCircle.x;
                spawnPoint.z = randomPointOnCircle.y;
                return spawnPoint;
        }
}