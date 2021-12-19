using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalProjectile : BaseProjectile
{
    [SerializeField]
    private float power;
    [SerializeField]
    private float lifespanTime;

    private new Rigidbody rigidbody;
    
    private Coroutine lifetimeCoroutine;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private IEnumerator LifespanCoroutine()
    {
        yield return new WaitForSeconds(lifespanTime);
        EndLifespan();
    }

    public override void Launch()
    {
        base.Launch();
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.forward * power, ForceMode.VelocityChange);
    }

    private void EndLifespan()
    {
        OnDeath?.Invoke(false);
    }

    public override void ReturnToPool()
    {
        base.ReturnToPool();
        rigidbody.velocity = Vector3.zero;
        if (lifetimeCoroutine != null)
        {
            StopCoroutine(lifetimeCoroutine);
            lifetimeCoroutine = null;
        }
    }

    public override void RequestFromPool()
    {
        base.RequestFromPool();
        rigidbody.velocity = Vector3.zero;
        lifetimeCoroutine = StartCoroutine(LifespanCoroutine());
    }

    private void OnCollisionEnter(Collision other)
    {
        HandleHitWith(other.gameObject);
        OnDeath?.Invoke(true);
    }
}