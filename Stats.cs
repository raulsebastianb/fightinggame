using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float health;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackDuration;

    Combat combatScript;
    public bool alive = true;
    public GameOverScreen gameOverScreen;
    public WinScreen winScreen;
    public GameObject enemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        combatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player" && health <= 0)
        {
            alive = false;
            Debug.Log("Destroyed for health<=0");
            Destroy(gameObject);
            combatScript.SetTargetable(null);
            GameOver();
        }

        if (health <= 0)
        {
            Debug.Log("Destroyed for health<=0");
            Destroy(gameObject);
            combatScript.SetTargetable(null);
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Win();
        }
    }

    public void GameOver()
    {
        gameOverScreen.Setup();
    }

    public void Win()
    {
        //Debug.Log("win");
        winScreen.Setup();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float newValue)
    {
        health = newValue;
    }

    public float GetAttackDuration()
    {
        return attackDuration;
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }
}
