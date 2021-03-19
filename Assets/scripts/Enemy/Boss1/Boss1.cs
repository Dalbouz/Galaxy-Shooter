using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField]
    private int _health = 70;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<Animator>().SetTrigger("IsHit");
            Destroy(other.gameObject,2.0f);
            _health--;
            
            
        }
        if(_health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    
}
