using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLaserBehaviour : MonoBehaviour
{

    private player _player;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<player>();
        if (_player == null)
            Debug.LogError("Player je jednak null.!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            _audioSource.Play();
        }
    }
}
