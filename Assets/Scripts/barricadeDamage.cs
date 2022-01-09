using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barricadeDamage : MonoBehaviour, IDamageable
{
    Transform rb;
    GameObject[] ChildrenOfObj;

    public bool isDestoryed;
    public float _objectHealth;
    public float startHealth = 100;
    private Vector3 _Prev;

    private void Awake()
    {
        rb = GetComponent<Transform>();
        _objectHealth = startHealth;
        isDestoryed = false;
    }

    public void TakeDamage(float damage)
    {
        _objectHealth -= damage;
        
        if(_objectHealth <= 0)
        {
            isDestoryed = true;

            Destroy(gameObject);
        }
    }
}
