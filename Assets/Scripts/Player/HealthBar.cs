using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(float healthValue)
    {
        slider.value = healthValue;
    }

    public void InitializeHealthBar(float healthValue)
    {
        ChangeMaxHealth(healthValue);
        ChangeCurrentHealth(healthValue);
    }
}
