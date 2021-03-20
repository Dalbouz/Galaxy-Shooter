using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1CannonScale : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftBossLeftCannon;
    [SerializeField]
    private GameObject _leftBossRightCannon;
    [SerializeField]
    private GameObject _rightBossLeftCannon;
    [SerializeField]
    private GameObject _rightBossRightCannon;

    private Vector3 _scaleChange = new Vector3(0, 0.1f, 0);
  

   
    void Update()
    {
        //FEEDBACK: izbjegavati magic numbers, hardkodirane vrijednosti
        if (_leftBossLeftCannon != null)
        {
            if (transform.localScale.y < 13)
                transform.localScale += _scaleChange;
        }
        else if (_leftBossRightCannon != null)
        {
            if (transform.localScale.y < 13)
                transform.localScale += _scaleChange;
        }
        else if (_rightBossLeftCannon != null)
        {
            if (transform.localScale.y < 13)
                transform.localScale += _scaleChange;
        }
        else if (_rightBossRightCannon != null)
        {
            if (transform.localScale.y < 13)
                transform.localScale += _scaleChange;
        }
    }

   
}
