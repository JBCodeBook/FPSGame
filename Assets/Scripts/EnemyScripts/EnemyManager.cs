using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;

    [SerializeField] 
    private float startingHealth;
    public float _currentHealth;

    public int damage = 5;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public barricadeDamage barricade;
    public GameObject targetSpot;
    public GameObject[] barricadeList;
    public GameObject player; 

    public int howManyBarricades;
    public bool inAttackRange;
    public bool isInteracting;
    public bool isAttacking;
    public bool isMelee;
    public bool isShooting;
    public float distanceToPlayer;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        barricadeList = GameObject.FindGameObjectsWithTag("Barricade");
        player = GameObject.FindGameObjectWithTag("Player");

        _currentHealth = startingHealth;
    }

    public void Update()
    {
        
        animator.SetFloat("SpeedParam", agent.velocity.magnitude);
        Debug.DrawLine(agent.destination, new Vector3(agent.destination.x, agent.destination.y + 1f, agent.destination.z), Color.red);

        if (inAttackRange)
        {
            _attackCounter += Time.deltaTime;
            attackBarricade();
        }

        if (isShooting)
        {
            _attackCounter += Time.deltaTime;
            attackPlayer();
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
            inAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inAttackRange = false;
    }

    public void attackBarricade()
    {
        if (_attackCounter >= _attackTime)
        {
            PlaytargetAnimation("MeleeAttack", true);
            barricade.TakeDamage(damage);
            _attackCounter = 0f;
        }

        if (barricade._objectHealth <= 0)
        {
            isAttacking = false;
            inAttackRange = false;
        }

        if (barricade == null)
        {
            Debug.Log("Barricade Null");
        }
    }

    public void attackPlayer()
    {
        if (_attackCounter >= _attackTime)
        {
            PlaytargetAnimation("Firing", true);
            Debug.Log("Firing Weapon");
            //player.TakeDamage(damage);
            _attackCounter = 0f;
        }
    }


    public void TakeDamage(float damage)
    {
        animator.Play("HitReaction");
        _currentHealth -= damage;
        healthBar.fillAmount = _currentHealth / startingHealth;

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        isMelee = animator.GetBool("isMelee");
    }

}
