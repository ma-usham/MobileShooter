using Darkmatter.Core;
using Darkmatter.Domain;
using System;
using UnityEngine;
using VContainer;

namespace Darkmatter.Presentation
{
    public class GunWeapon : MonoBehaviour, IReloadableWeapon
    {
        [Header("VFX")]
        public ParticleSystem MuzzleFlashParticle;
        public ParticleSystem BulletHitEffectParticle;
        public ParticleSystem ZombieHitEffectParticle;

        [Header("Weapon Data")]
        public float fireRate = 0.1f;
        public bool isReloading { get; set; }
        public int AmmoCount { get; set; } = 40;
        public int initialAmmoCount { get; set; } = 40;
        private float lastUsedTime;
        public GameObject BulletHole;   
        public bool canAttack => Time.time >= lastUsedTime + fireRate && AmmoCount > 0 && !isReloading;

        [Inject] private ITargetProvider targetProvider;
        private RaycastHit hitPoint => targetProvider.hitPoint;


        public void Attack()
        {
            lastUsedTime = Time.time;
            AmmoCount--;

            PlayMuzzleFlash();
            if (hitPoint.transform != null) PlayBulletHitEffectParticle();
        }

        private void ShowBulletHole(Vector3 spawnPos)
        {
            GameObject bulletHit = Instantiate(BulletHole, spawnPos, Quaternion.LookRotation(hitPoint.normal));
            Destroy(bulletHit, 5f);
        }

        private void PlayBulletHitEffectParticle()
        {
            var damageable = hitPoint.transform.GetComponent<IDamageable>();
            Debug.Log(hitPoint.transform);

            Vector3 particleSpawnPos = hitPoint.point + hitPoint.normal * 0.01f;
            Quaternion ParticleRotation = Quaternion.LookRotation(hitPoint.normal);

            if (damageable != null)
            {
                ZombieHitEffectParticle.transform.position = particleSpawnPos;
                ZombieHitEffectParticle.transform.rotation = ParticleRotation;
                ZombieHitEffectParticle.Play(true);
                damageable.TakeDamage(10f);
            }
            else
            {
                BulletHitEffectParticle.transform.position = particleSpawnPos;
                BulletHitEffectParticle.transform.rotation = Quaternion.LookRotation(hitPoint.normal);
                BulletHitEffectParticle.Play(true);
            }
    
        }

        public void Reload()
        {
            AmmoCount = initialAmmoCount;
        }

        private void PlayMuzzleFlash()
        {
            MuzzleFlashParticle.Play(true);
        }
    }
}
