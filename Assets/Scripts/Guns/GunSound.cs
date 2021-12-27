using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunSound : MonoBehaviour
{
        [SerializeField]
        private AudioClip shootClip;

        private AudioSource audioSource;

        private void Awake()
        {
                audioSource = GetComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.Stop();
        }

        public void Start()
        {
                var gun = GetComponent<Gun>();
                gun.Shooter.OnShoot += PlayShootSound;
        }

        private void PlayShootSound()
        {
                audioSource.PlayOneShot(shootClip);
        }
}