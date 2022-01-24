using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float _spd = 0.0000001f;
    Rigidbody rb;
    FireArmScript fireArmScript;

/*    [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
    public GameObject projectilePrefab;*/

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        fireArmScript = GetComponentInParent<FireArmScript>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //HandleHit(other);
        Destroy(gameObject, 5f);

    }

/*    void HandleHit(Collider other)
    {
        GameObject spawnedDecal = GameObject.Instantiate(projectilePrefab, other.transform.position, Quaternion.LookRotation(other.transform.position.normalized));
        spawnedDecal.transform.SetParent(other.transform);
    }*/
}