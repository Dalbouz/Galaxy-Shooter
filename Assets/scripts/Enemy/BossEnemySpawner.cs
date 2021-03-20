using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    private float _numberOfHitsToDestroy = 10f;
    private float _numberOfHits = 0;
    private Vector3 _endPosition = new Vector3(0, 4.7f, 0);
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _storyText;
    
   
    void Start()
    {
        //FEEDBACK: izbjegavati Find metode, koristiti reference
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
            Debug.LogError("_spawnManager je jednak NULL.");
        transform.GetComponent<EdgeCollider2D>().enabled = false;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("UIManager je jednak null");
    }

    // Update is called once per frame
    void Update()
    {

       // MovementEnd();
        if (transform.position != _endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);    
         }
        
        if(_numberOfHits >=_numberOfHitsToDestroy) 
            MovementEnd();


    }
   

    

    private void MovementEnd()
    {
        
        transform.position += Vector3.up * _speed * 2* Time.deltaTime;
        if (transform.position.y >= 11)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<Animator>().SetTrigger("IsHit");
            _numberOfHits += 1;
            Destroy(other.gameObject,2.0f);
   
            if (_numberOfHits == _numberOfHitsToDestroy)
            {
                _spawnManager.StartAllCoroutines();
            }
           
        }
    }

    public void EnableCollider()
    {
        transform.GetComponent<EdgeCollider2D>().enabled = true;
        
        Destroy(_storyText);
    }
}
