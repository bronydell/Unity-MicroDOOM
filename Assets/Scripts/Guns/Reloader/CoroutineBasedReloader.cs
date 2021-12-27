using System.Collections;
using UnityEngine;

public class CoroutineBasedReloader : Reloader
{
    [SerializeField] 
    private float reloadTime;

    private Coroutine reloadingCoroutine;

    public override void StartReloading()
    {
        base.StartReloading();
        reloadingCoroutine ??= StartCoroutine(ReloadCoroutine());
    }

    public override bool IsReloading()
    {
        return reloadingCoroutine != null;
    }

    public override void CancelReloading()
    {
        base.CancelReloading();
        if (reloadingCoroutine == null) return;

        StopCoroutine(reloadingCoroutine);
        FinishReloading();
    }

    private void FinishReloading()
    {
        reloadingCoroutine = null;
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);
        targetGun.State.ResetAmmo();
        FinishReloading();
    }
}