using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { PATROLLING, CHASING, SEARCHING, ATTACKING };

    public NavMeshAgent agent;
    public Transform player;

    public float health;

    bool isPatrolling;
    bool isChasing;
    bool isSearching;
    bool isAttacking;

    public LayerMask whatIsGround, whatIsPlayer;
    private EnemyState enemyState = EnemyState.PATROLLING;
    private Animator animator;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("LowPolyWithGun").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        enemyState = EnemyState.PATROLLING;
        animator.SetInteger("State", 0);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        enemyState = EnemyState.CHASING;
        animator.SetInteger("State", 0);

        agent.SetDestination(player.position);
        isChasing = true;
    }

    private void SearchWalkPoint()
    {
        enemyState = EnemyState.SEARCHING;
        animator.SetInteger("State", 2);

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void AttackPlayer()
    {
        enemyState = EnemyState.ATTACKING;
        animator.SetInteger("State", 3);

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);          

        transform.LookAt(player);

        isAttacking = true;

        if (!alreadyAttacked)
        {

            //  Attack code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.up * 32f, ForceMode.Acceleration.Impulse);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.Play("HitReaction");
        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }



}
