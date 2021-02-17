﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8.0f;

  

    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Animator Component jednak NULL");
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);
       
        

        if (transform.position.y >= 8.0f) //ako izađe izvan ekrana igrice
        {
            if (transform.parent != null) // provjeravamo da li objekt na kojem je ova skripta ima parent (odnosno da ni je tvrdnja transfrom.parent razlicita od null) ako je razlicita znaci da ima parent
            {
                Destroy(transform.parent.gameObject); // unistavamo roditelj tripple shot prefab
            }
                Destroy(this.gameObject); // a ako nije unistavamo samo instancirani laser prefab
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boss")
        {
            _animator.SetTrigger("IsHit");
        }
    }
}
