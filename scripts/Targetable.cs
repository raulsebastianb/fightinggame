using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Targetable : MonoBehaviour
{
    public enum EnemyType { Minion }
    private EnemyType enemyType;

    public void SetEnemyType(EnemyType newValue)
    {
        enemyType = newValue;
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}
