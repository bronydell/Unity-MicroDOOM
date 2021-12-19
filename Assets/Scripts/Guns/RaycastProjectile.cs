using UnityEngine;

public class RaycastProjectile : BaseProjectile
{
    [SerializeField] 
    private bool debugMode = false;
    public override void Launch()
    {
        base.Launch();
        if (debugMode)
        {
            Debug.DrawRay(transform.position, Vector3.forward * 100.0f, Color.red);
        }

        var projectileTransform = transform;
        bool didHit = Physics.Raycast(projectileTransform.position, projectileTransform.forward, out RaycastHit hitInfo);
        if (didHit)
        {
            HandleHitWith(hitInfo.transform.gameObject);
        }
    
        OnDeath(didHit);
    }
}