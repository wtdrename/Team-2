using System;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    public PlayerManager playerManager;
    private Vector3 shootDir;
    private GameObject target;

    public GameObject vfxMuzzle;
    public void Setup(Vector3 shootDirection)
    {
        this.shootDir = shootDirection;
        playerManager = PlayerManager.instance;
        if(vfxMuzzle != null)
        {
            var muzzle = Instantiate(vfxMuzzle, transform.position, Quaternion.identity);
            muzzle.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzle.GetComponent<ParticleSystem>();
            if(psMuzzle != null)
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
        Destroy(gameObject, 5f);
    }

    private void CollidedWithAttackable(GameObject target)
    {
        playerManager.OnProjectileCollided(target);
    }

    private void Update()
    {
        float moveSpeed = 10f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<IAttackable>() != null)
        {
            CollidedWithAttackable(collider.gameObject);
            Destroy(gameObject);
        }

    }
}
