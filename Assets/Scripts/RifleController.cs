using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : Weapon
{
    CameraManager cameraManager;
    FireArmScript fireArmScript;

    public GameObject impactEffect;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        fireArmScript = FindObjectOfType<FireArmScript>();
    }

    public override void Use()
    {
        Shoot();
    }

    public void Shoot()
    {
        float damage = ((GunInfo)itemInfo).damage;
        float range = ((GunInfo)itemInfo).range;

        if (fireArmScript == null)
        {
            fireArmScript = FindObjectOfType<FireArmScript>();
        }

        fireArmScript.FireWeapon(); 

        RaycastHit hit;
        if (Physics.Raycast(cameraManager.transform.position, cameraManager.transform.forward, out hit, range))
        {

            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);

            EnemyAITree enemryAI= hit.transform.GetComponent<EnemyAITree>();

            if (enemryAI != null)
            {
                
                enemryAI.TakeDamage(damage);
            }

            GameObject impactGO= Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }


}
