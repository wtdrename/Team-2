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
public PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void ShootWeapon(bool isRaycast)
    {

        if (isRaycast)
            ShootRaycastBullet();
        else if(!isRaycast)
            ShootingProjectileBullet();
    }


    public void ShootingProjectileBullet()
    {
        Transform bullet = Instantiate(pfBullet, gunEndPosition.position, Quaternion.identity);
        shootDir = gunEndPosition.forward;
        bullet.localRotation = Quaternion.LookRotation(gunEndPosition.forward);
        bullet.GetComponent<Projectile>().Setup(shootDir);
        PlayerManager.Instance.ShootingAnimation();

    }

    public void ShootRaycastBullet()
    {
        shootDir = gunEndPosition.forward;
        RaycastHit raycastHit;
        if (Physics.Raycast(gunEndPosition.transform.position, shootDir, out raycastHit, 100f))
        {
            if (raycastHit.transform.gameObject.GetComponent<IAttackable>() != null)
            {

                playerManager.OnProjectileCollided(raycastHit.transform.gameObject);
            }
            
        }

    }
}
