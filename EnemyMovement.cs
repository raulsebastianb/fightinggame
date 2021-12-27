using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    private float rotateSpeed = 0.1f;
    public float rotateVelocity;

    private EnemyAttacks combatScript;

    public GameObject player;

    [SerializeField]
    GameObject playerToFollow;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        combatScript = GetComponent<EnemyAttacks>();
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (combatScript.GetTargetable() != null)
        {
            if (combatScript.GetTargetable().GetComponent<Combat>() != null)
            {
                if (!combatScript.GetTargetable().GetComponent<Combat>().GetAlive())
                {
                    combatScript.SetTargetable(null);
                }
            }
        }

        if (Vector3.Distance(gameObject.transform.position, playerToFollow.transform.position) <= combatScript.attackRange)
        {
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            //combatScript.targetedEnemy = player;
            // rotation
            Quaternion rotationLook = Quaternion.LookRotation(playerToFollow.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                rotationLook.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeed * (Time.deltaTime * 5));

            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
        else
        {
            agent.isStopped = false;
            //combatScript.targetedEnemy = null;
            agent.SetDestination(playerToFollow.transform.position);
        }
    }

    public void SetAgent(NavMeshAgent newValue)
    {
        agent = newValue;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}
