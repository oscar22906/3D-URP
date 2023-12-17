using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Breeze.Core;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public float bulletDamage = 20;

    public GameObject player;
    public bool isFiring = false;
    public float fireRate = 0.5f;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f; 
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public WeaponRecoil recoil;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifetime = 3.0f;

    public RandomSounds soundScript;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
        soundScript = GetComponent<RandomSounds>();
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        if(accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime = 0.0f;
        }


    }

    public void UpdateFiring(float deltaTime)
    {
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }

    }

    public void UpdateBullets(float deltaTime)
    {
        accumulatedTime += deltaTime;
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifetime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //old code
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            if (hitInfo.transform.GetComponent<BreezeDamageBase>() != null)
            {
                hitInfo.transform.GetComponent<BreezeDamageBase>().TakeDamage(bulletDamage, player, true);
                Debug.Log("Raycast Fired and hit Breeze AI person thing :)");
            }

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);                                                      // Muzzle flash

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifetime; 
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }

    private void FireBullet()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
        soundScript.GunSound();

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

        recoil.GenerateRecoil();



    }



    public void StopFiring()
    {
        isFiring = false;
    }

}