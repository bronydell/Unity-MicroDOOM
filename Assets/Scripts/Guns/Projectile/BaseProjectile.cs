using System;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour, IPoolable
{
    [SerializeField]
    private int damage;
    // Bool = didHit anything?
    public Action<bool> OnDeath;

    public virtual void Launch()
    {
        
    }

    public virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public virtual void RequestFromPool()
    {
        gameObject.SetActive(true);
    }

    protected void HandleHitWith(GameObject damagedObject)
    {
        var damageable = damagedObject.GetComponent<IDamageable>() ?? damagedObject.GetComponentInParent<IDamageable>();
        damageable?.OnHit(gameObject, damage);
    }
}