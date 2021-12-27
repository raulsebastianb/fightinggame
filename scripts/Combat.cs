using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public enum AttackType { Melee, Ranged };
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float rotateSpeedForAttack;

    private CharacterMovement movementScript;
    private Stats statsScript;
    private Animator animator;

    [SerializeField]
    private bool basicAttack = false;
    [SerializeField]
    private bool alive;
    [SerializeField]
    private bool meleeAttack = true;

    public GameOverScreen gameOverScreen;
    
    [SerializeField]
    private GameObject targetedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<CharacterMovement>();
        statsScript = GetComponent<Stats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                //Debug.Log(transform.position + " " + targetedEnemy.transform.position + " " + attackRange);
                movementScript.GetAgent().SetDestination(targetedEnemy.transform.position);
                movementScript.GetAgent().stoppingDistance = attackRange;

                Quaternion rotationLook = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    rotationLook.eulerAngles.y,
                    ref movementScript.rotateVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                movementScript.GetAgent().SetDestination(transform.position);
                if (attackType == AttackType.Melee)
                {
                    if(meleeAttack)
                    {
                        // attack
                        StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
    }

    IEnumerator MeleeAttackInterval()
    {
        meleeAttack = false;
        //Debug.Log("melee false");
        animator.SetBool("Melee", true);
        yield return new WaitForSeconds(statsScript.GetAttackDuration() / ((100 + statsScript.GetAttackDuration()) * 0.01f));

        MeleeAttack();

        if(targetedEnemy == null)
        {
            animator.SetBool("Melee", false);
            meleeAttack = true;
            //Debug.Log("melee true");
        }
    }

    public void MeleeAttack()
    {
        if (targetedEnemy != null)
        {
            if(targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.Minion)
            {
                targetedEnemy.GetComponent<Stats>().SetHealth(targetedEnemy.GetComponent<Stats>().GetHealth() - statsScript.GetAttackDamage());
            }
        }
        meleeAttack = true;
        //Debug.Log("melee true");
    }

    public void SetTargetable(GameObject newValue)
    {
        targetedEnemy = newValue;
    }

    public GameObject GetTargetable()
    {
        return gameObject;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public void SetMeleeAttack(bool newValue)
    {
        meleeAttack = newValue;
    }
}
