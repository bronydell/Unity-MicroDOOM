using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
        [SerializeField]
        protected Shooter shooter;

        public virtual void Shoot()
        {
                shooter.Shoot();
        }
}