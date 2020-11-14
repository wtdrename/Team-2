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

    public FieldOFView fieldOFView;
    public GameObject vfxMuzzle;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        fieldOFView = GetComponent<FieldOFView>();

    }

    public void ShootWeapon(bool isRaycast)
    {

       if (fieldOFView.getClosestEnemy())
       {
            if (isRaycast)
                ShootRaycastBullet();
            else if (!isRaycast)
                ShootProjectileBullet();
        }
       
    }

    public void ShootProjectileBullet()
    {
        Transform bullet = Instantiate(pfBullet, gunEndPosition.position, Quaternion.identity);
        MuzzleFlashAnimation();
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
                MuzzleFlashAnimation();
            }            
        }      
    }

    public void MuzzleFlashAnimation()
    {
        
            if (vfxMuzzle != null)
            {
                var muzzle = Instantiate(vfxMuzzle, gunEndPosition.position, Quaternion.identity);
                muzzle.transform.forward = gameObject.transform.forward;
                var psMuzzle = muzzle.GetComponent<ParticleSystem>();
                if (psMuzzle != null)
                {
                    psMuzzle.Play();
                    Destroy(muzzle, psMuzzle.main.duration);
                }
                else
                {
                    var psChild = muzzle.GetComponent<ParticleSystem>();
                    Destroy(muzzle, psChild.main.duration);
                }
            }
        
    }
}
