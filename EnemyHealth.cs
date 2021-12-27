using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider enemySlider3D;

    Stats statsScript;

    void Start()
    {
        statsScript = GetComponentInParent<Stats>();
        enemySlider3D.maxValue = statsScript.GetMaxHealth();
        statsScript.SetHealth(statsScript.GetMaxHealth());
    }

    void Update()
    {
        enemySlider3D.value = statsScript.GetHealth();
    }
}
