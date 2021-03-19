using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float _health = 3;

    private Animator _animator;
    private player _player;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    private LaserMovement _laserMovement;

    private UIManager _uiManager;

    private int _announcementUI;


    private int _positiveOrNegativeRotation;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("UiManager je jednak Null");
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Animator je jednak NULL.");
        _player = GameObject.Find("Player").GetComponent<player>();
        if (_player == null)
            Debug.LogError("_player je jednak NULL.");
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
            Debug.Log("_spawnManager je jednak NULL.");
        _audioSource = gameObject.GetComponent<AudioSource>();
        
        

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
            other.gameObject.GetComponent<Animator>().SetTrigger("IsHit");
            Destroy(other.gameObject, 2.0f);
            _health -= 1;
            
            if (_health <= 0)
            {
                _animator.SetTrigger("IsDestroy");
                _speed = 0;
                _audioSource.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.0f);
                int Rand = Random.Range(0, 3);
                _spawnManager.SpawnEnemyWithAsteroid(Rand);
                 _uiManager.TurnOnAsteroidAnnouncment(Rand);
            }
        }

        if(other.tag == "Player")
        {
            _animator.SetTrigger("IsDestroy");
            _speed = 0;
            _audioSource.Play();
            _player.Damage();
            Destroy(this.gameObject,2.0f);
        }

    }

   
}
