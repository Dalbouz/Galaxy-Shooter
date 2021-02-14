using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _speed = 2.0f;

    private Animator _animator;
    private player _player;
    private SpawnManager _spawnManager;

    private int _positiveOrNegativeRotation;
    // Start is called before the first frame update
    void Start()
    { 

        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Animator je jednak NULL.");
        _player = GameObject.Find("Player").GetComponent<player>();
        if (_player == null)
            Debug.LogError("_player je jednak NULL.");
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
            Debug.Log("_spawnManager je jednak NULL.");

        _positiveOrNegativeRotation = Random.Range(0, 2);
        if (_positiveOrNegativeRotation == 0)
            _rotationSpeed = -6;
        else
            _rotationSpeed = 6;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime); //rotiranje objekta određenom brzinom oko sebe
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            _animator.SetTrigger("IsDestroy");
            _speed = 0; 
            Destroy(other.gameObject);
            Destroy(this.gameObject,2.0f);
            _spawnManager.SpawnEnemyWithAsteroid();
            
        }

        if(other.tag == "Player")
        {
            _animator.SetTrigger("IsDestroy");
            _speed = 0;
            _player.Damage();
            Destroy(this.gameObject,2.0f);
        }

    }

   
}
