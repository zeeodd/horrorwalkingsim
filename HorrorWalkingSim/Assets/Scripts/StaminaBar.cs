using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private float initStamina = 100f;
    public float currStamina;
    public Slider slider;

    void Start()
    {
        currStamina = initStamina;
    }

    void Update()
    {
        currStamina += 0.075f;
        slider.value = currStamina;

        if (currStamina >= 100)
        {
            currStamina = 100;
        }
    }

    public void depleteStamina(float amt)
    {
        currStamina -= amt;
        slider.value = currStamina;
    }
}
