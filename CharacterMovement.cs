using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    private float rotateSpeed = 20f;
    public float rotateVelocity;

    private Combat combatScript;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        combatScript = GetComponent<Combat>();
    }

    void Update()
    {
        if (combatScript.GetTargetable() != null)
        {
            if(combatScript.GetTargetable().GetComponent<Combat>() != null)
            {
                if (!combatScript.GetTargetable().GetComponent<Combat>().GetAlive())
                {
                    combatScript.SetTargetable(null);
                }
            }
        }

        // when pressing the right mouse button
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            // if raycast shot hits the navmesh system
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "Floor")
                {
                    // move the player to said point
                    agent.SetDestination(hit.point);
                    combatScript.SetTargetable(null);
                    agent.stoppingDistance = 0;

                    // rotation
                    Quaternion rotationLook = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(
                        transform.eulerAngles.y,
                        rotationLook.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeed * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
            }
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
