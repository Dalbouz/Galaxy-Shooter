using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannonLeftRight : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private GameObject _leftCannon;
    [SerializeField]
    private GameObject _rightCannon;
    [SerializeField]

    private player _player;
    


    private void Start()
    {

        _player = GameObject.Find("Player").GetComponent<player>();
        if (_player == null)
            Debug.LogError("Player je jednak Null.!");

    }
    // Update is called once per frame
    void Update()
    {
        if (_rightCannon != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(9.3f, transform.position.y, transform.position.z), _speed * Time.deltaTime);
        else if (_leftCannon != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-9.3f, transform.position.y, transform.position.z), _speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            _player.Damage();
    }


}
