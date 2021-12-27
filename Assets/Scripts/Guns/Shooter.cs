using UnityEngine;

public delegate void ShootHandler();

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;
    
    protected Gun targetGun;
    
    private ObjectPool<BaseProjectile> projectilePool;

    public event ShootHandler OnShoot;
    
    public void Setup(Gun gun)
    {
        targetGun = gun;
    }

    private void Awake()
    {
        projectilePool = new ObjectPool<BaseProjectile>(CreateProjectile);
    }

    public bool Shoot()
    {
        if (targetGun.State.CurrentAmmo > 0)
        {
            targetGun.State.CurrentAmmo--;
        }
        else
        {
            // Not enough ammo!
            return false;
        }
        var projectile = projectilePool.GetObjectFromPool();
        projectile.OnDeath = (result) =>
        {
            projectilePool.ReturnObjectToPool(projectile);
        };
        var projectileSpawnTransform = projectileSpawnPoint.transform;
        projectile.transform.SetPositionAndRotation(projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        projectile.Launch();
    
        OnShoot?.Invoke();
        
        return true;
    }

    protected virtual BaseProjectile CreateProjectile()
    {
        return Instantiate(projectilePrefab).GetComponent<BaseProjectile>();
    }
}