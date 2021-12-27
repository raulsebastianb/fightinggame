using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public enum AttackType { Melee, Ranged };
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    public float attackRange;
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

    [SerializeField]
    public GameObject targetedEnemy;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start " + this.gameObject);
        movementScript = GetComponent<CharacterMovement>();
        statsScript = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= attackRange)
        {
            targetedEnemy = player;
        }
        else
        {
            targetedEnemy = null;
        }

        if (targetedEnemy != null)
        {
            //movementScript.GetAgent().SetDestination(transform.position);
            if (attackType == AttackType.Melee)
            {
                if (meleeAttack)
                {
                    StartCoroutine(MeleeAttackInterval());
                }
            }
        }
        
    }

    IEnumerator MeleeAttackInterval()
    {
        meleeAttack = false;
        animator.SetBool("Melee", true);
        yield return new WaitForSeconds(statsScript.GetAttackDuration() / ((100 + statsScript.GetAttackDuration()) * 0.01f));

        MeleeAttack();

        if (targetedEnemy == null)
        {
            animator.SetBool("Melee", false);
            meleeAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (targetedEnemy != null)
        {
            targetedEnemy.GetComponent<Stats>().SetHealth(targetedEnemy.GetComponent<Stats>().GetHealth() - statsScript.GetAttackDamage());
        }
        meleeAttack = true;
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

    void OnDestroy()
    {
        Debug.Log("Destroyed " + this.gameObject);
    }
}
