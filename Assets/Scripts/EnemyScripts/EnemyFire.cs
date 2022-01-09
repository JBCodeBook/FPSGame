using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyFire : MonoBehaviour
{
    CameraManager cameraManager;
    FireArmScript gunFireController;

    public GameObject impactEffect;


    public float damage = 10f;
    public float range = 100f;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        gunFireController = FindObjectOfType<FireArmScript>();
    }

    public void Shoot()
    {
        gunFireController.FireWeapon();

        RaycastHit hit;
        if (Physics.Raycast(cameraManager.transform.position, cameraManager.transform.forward, out hit, range))
        {
/*            if (playerManager != null)
            {
                enemryAI.TakeDamage(damage);
            }*/

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
