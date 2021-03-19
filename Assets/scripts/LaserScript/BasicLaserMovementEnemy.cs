using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaserMovementEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    
    private Animator _animator;
    private AudioSource _audioSource;
    private player _player;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("ANimator not Found");
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.LogError("AudioSource not found");
        _player = GameObject.Find("Player").GetComponent<player>();
        if (_player == null)
            Debug.LogError("Player is not found");
            
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime); //kretanje prema dolje 

        if (transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _speed = 0;
            _audioSource.Play();
            _animator.SetTrigger("IsHit");
            _player.Damage();
            Destroy(this.gameObject, 2.0f);
        }   
    }

}
