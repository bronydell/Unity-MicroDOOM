using System;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public abstract class Reloader : MonoBehaviour
{
    protected Gun targetGun;

    public void Setup(Gun gun)
    {
        targetGun = gun;
    }

    public virtual void StartReloading() {}

    public virtual void CancelReloading() { }

    public virtual bool IsReloading()
    {
        return false; 
    }
}