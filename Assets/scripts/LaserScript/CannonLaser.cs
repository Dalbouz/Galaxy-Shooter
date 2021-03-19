using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLaser : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftLaser;
    [SerializeField]
    private GameObject _rightLaser;
    
    private Vector3 _scaleChange = new Vector3(0.1f, 0f, 0);

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_leftLaser != null)
        {
            if(transform.localScale.x<12)
            transform.localScale += _scaleChange;
           
        }
        else if (_rightLaser != null)
        {
            if(transform.localScale.x>-34f)
            transform.localScale -= _scaleChange;
        }

    }
}
