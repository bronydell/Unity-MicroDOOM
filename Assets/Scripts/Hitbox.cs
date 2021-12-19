using System;
using UnityEngine;


public delegate void HitHandler(GameObject targetObject);

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
    public event HitHandler OnHit;

    private void OnTriggerEnter(Collider other)
    {
        OnHit?.Invoke(other.gameObject);
    }
}