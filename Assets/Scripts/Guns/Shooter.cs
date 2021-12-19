using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;

    private ObjectPool<BaseProjectile> projectilePool;
    
    private void Awake()
    {
        projectilePool = new ObjectPool<BaseProjectile>(CreateProjectile);
    }

    public void Shoot()
    {
        var projectile = projectilePool.GetObjectFromPool();
        projectile.OnDeath = (result) =>
        {
            projectilePool.ReturnObjectToPool(projectile);
        };
        var projectileSpawnTransform = projectileSpawnPoint.transform;
        projectile.transform.SetPositionAndRotation(projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        projectile.Launch();
    }

    protected virtual BaseProjectile CreateProjectile()
    {
        return Instantiate(projectilePrefab).GetComponent<BaseProjectile>();
    }
}