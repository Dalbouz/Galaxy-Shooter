using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    
    public void StartSlider(int StartHealth)
    {
        _slider.maxValue = StartHealth;
        _slider.value = StartHealth;
    }

    public void SliderHealth(int health)
    {
        _slider.value = health;
    }

    
}
