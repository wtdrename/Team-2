using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ProjectileManager : MonoBehaviour
{
    public Transform pfBullet;

    public Transform gunEndPosition;

    public Transform shootPosition;
    public Vector3 shootDir;


    public void ShootingProjectile()
    {
        
        Transform bullet = Instantiate(pfBullet, gunEndPosition.position, Quaternion.identity);
        shootDir = gunEndPosition.forward;
        bullet.localRotation = Quaternion.LookRotation(gunEndPosition.forward);
        bullet.GetComponent<Projectile>().Setup(shootDir);
    }

}
