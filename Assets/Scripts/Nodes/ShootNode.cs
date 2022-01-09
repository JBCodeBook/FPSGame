using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{
    private NavMeshAgent agent;
    private EnemyAITree ai;
    private Transform target;
    Animator animator;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    private Vector3 currentVelocity;
    private float smoothDamp;

    public ShootNode(NavMeshAgent agent, EnemyAITree ai, Transform target, Animator animator)
    {
        this.agent = agent;
        this.ai = ai;
        this.target = target;
        this.animator = animator;
        smoothDamp = 1f;

    }

    private void targetPlayer()
    {
        //animator.Play("Firing");
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;
        ai.SetColor(Color.green);

        Vector3 direction = target.position - ai.transform.position;
        Vector3 currentDirection = Vector3.SmoothDamp(ai.transform.forward, direction, ref currentVelocity, smoothDamp);
        Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);

        Debug.DrawRay(ai.transform.position, direction);
        ai.transform.rotation = rotation;

        targetPlayer();



        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            _attackCounter = 0f;
        }

        return NodeState.RUNNING;
    }

}