using System.Collections;
using System.Collections.Generic;
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

        private List<Enemy> activeEnemies;

        private Coroutine spawnCoroutine;

        private void Awake()
        {
                enemyPool = new ObjectPool<Enemy>(SpawnEnemy);
                activeEnemies = new List<Enemy>();
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

        public void StopAllEnemies()
        {
                foreach (var enemy in activeEnemies)
                {
                        enemy.StopThinking();
                }
        }

        public void RecycleAllEnemies()
        {
                foreach (var enemy in activeEnemies)
                {
                        enemyPool.ReturnObjectToPool(enemy);
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
                                activeEnemies.Remove(enemy);
                                enemy.StopThinking();
                                enemyPool.ReturnObjectToPool(enemy);
                        };
                        enemy.transform.position = GetRandomPosition();
                        enemy.StartThinking();
                        activeEnemies.Add(enemy);
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