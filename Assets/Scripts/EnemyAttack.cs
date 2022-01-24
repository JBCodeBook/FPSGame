using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyManager enemyManager;
    public GameObject player;
    public Animator animator;
    public GameObject[] barricadeList;
    public barricadeDamage barricade;


    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    public int damage = 5; 

    public int howManyBarricades;
    public bool inAttackRange;
    public bool isMelee;
    public bool isInteracting;
    public float distanceToPlayer;
    public bool atBarricade;
    public bool isAttacking;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (atBarricade && isAttacking)
        {
            _attackCounter += Time.deltaTime;
            attackBarricade();
        }

        howManyBarricades = barricadeList.Length;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    public void attackBarricade()
    {
            Debug.Log("Attacking barricade");
            if (_attackCounter >= _attackTime && !isMelee)
            {
                enemyManager.PlaytargetAnimation("MeleeAttack", true);
                barricade.TakeDamage(damage);
                _attackCounter = 0f;
            }

            if (barricade._objectHealth <= 0)
            {
                atBarricade = false;
                isAttacking = false;
                inAttackRange = false;
            }

            if (barricade == null)
            {                
                Debug.Log("Barricade Null");
            }
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        isMelee = animator.GetBool("isMelee");
    }

}
