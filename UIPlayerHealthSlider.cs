using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthSlider : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    Stats statsScript;
    
    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerSlider2D = GetComponent<Slider>();
        playerSlider3D.maxValue = statsScript.GetMaxHealth();
        playerSlider2D.maxValue = statsScript.GetMaxHealth();
        statsScript.SetHealth(statsScript.GetMaxHealth());
    }

    void Update()
    {
        playerSlider2D.value = statsScript.GetHealth();
        playerSlider3D.value = playerSlider2D.value;
    }
}
