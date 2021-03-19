using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineHealth : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private GameObject _engineDamageImage;

    private bool _isEngineDown = false;

    
    

    private void Start()
    {
        _slider.maxValue = 2;
        
    }

    public void EngineStart(int EngineStart)
    {
        _slider.value = EngineStart;
    }

    public void EngineDamage(int EngineDamage)
    {
        _isEngineDown = true;
        _slider.value = EngineDamage;
        
        if (EngineDamage == 2)
            StartCoroutine(EngineDownBlink());
    }

    IEnumerator EngineDownBlink()
    {
        while (_isEngineDown == true)
        {

        _engineDamageImage.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        _engineDamageImage.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        }
    }

    public void HealEngine(int HealEngine)
    {
        _slider.value = HealEngine;
        _isEngineDown = false;
    }
}
