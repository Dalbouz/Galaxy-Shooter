using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannonParentMove : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private GameObject _leftlaserPrefab;
    [SerializeField]
    private GameObject _rightLaserPrefab;


    private bool _isLaserSpawned = false;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LaserSpawner());
        StartCoroutine(MoveDownRoutine());
       
    }
    public void MoveDown()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        if (transform.position.y <= -8f)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator LaserSpawner()
    {
        if (_isLaserSpawned == false)
        {

            yield return new WaitForSeconds(2f);
            _leftlaserPrefab.SetActive(true);
            _rightLaserPrefab.SetActive(true);
            _isLaserSpawned = true;
            
            
        }


    }

    IEnumerator MoveDownRoutine()
    {
        yield return new WaitForSeconds(5.5f);
        MoveDown();
    }
}
