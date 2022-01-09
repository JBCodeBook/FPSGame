using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;

    public bool atBarricade;
    public bool isAttacking;

    public int damage = 5;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public barricadeDamage barricade;
    public GameObject targetSpot;
    public GameObject[] barricadeList;
    public GameObject player; 

    public int howManyBarricades;
    public bool isMelee;
    public bool isInteracting;
    public float distanceToPlayer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        barricadeList = GameObject.FindGameObjectsWithTag("Barricade");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        
        animator.SetFloat("SpeedParam", agent.velocity.magnitude);
        if (atBarricade && isAttacking)
        {
            _attackCounter += Time.deltaTime;
            attackBarricade();
        }

        howManyBarricades = barricadeList.Length;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    public void PlaytargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        targetSpot = other.gameObject;
        barricade = other.GetComponentInParent<barricadeDamage>();

        if(barricade != null)
        {
            atBarricade = true;
            isAttacking = true;
        }
    }


    public void attackBarricade()
    {
        if (atBarricade)
        {

            if (_attackCounter >= _attackTime && !isMelee)
            {
                PlaytargetAnimation("MeleeAttack", true);
                barricade.TakeDamage(damage);
                _attackCounter = 0f;
            }
            
            if (barricade._objectHealth <= 0)
            {
                Debug.Log("Barricade destoryed");

                atBarricade = false;
                isAttacking = false;
            }

            if (barricade == null)
            {
                Debug.Log("Barricade Null");
            }
        } 
    }

    public void attackPlayer()
    {
    }

    private void LateUpdate()
    {   
        isInteracting = animator.GetBool("isInteracting");
        isMelee = animator.GetBool("isMelee");
    }

}
