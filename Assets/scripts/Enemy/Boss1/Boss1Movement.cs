using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    private Vector3 _endPosition = new Vector3(0, 8f, 0);
    [SerializeField]
    private GameObject _leftLeftCannon;
    [SerializeField]
    private GameObject _leftRightCannon;
    [SerializeField]
    private GameObject _rightLeftCannon;
    [SerializeField]
    private GameObject _rightRightcannon;

    private GameObject _leftLeftCannonPrefab;
    private GameObject _leftRightCannonPrefab;
    private GameObject _rightLeftCannonPrefab;
    private GameObject _rightRightCannonPrefab;

    private int _numberOfHits;

    

    void Start()
    {
        StartCoroutine(SpawnCannon());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != _endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);
        }

        
        if (_numberOfHits >= 300)
            Destroy(this.gameObject);
    }

      IEnumerator SpawnCannon()
        {
        Vector3 PosToSpawnLeftLeft = new Vector3(-5f, 2.48f, -4.35f);
        Vector3 PosToSpawnLeftRight = new Vector3(-1.1f, 2.44f, -2.84f);
        Vector3 PosToSpawnRightLeft = new Vector3(1.62f, 2.41f, -4f);
        Vector3 PosToSpawnRightRight = new Vector3(5.36f, 2.38f, -4.9f);
            while (_numberOfHits<300)
            {
            
            int RandCannon = Random.Range(0, 4);
            float RandomTimeWaitCannon = Random.Range(2, 6);
            switch (RandCannon)
            {
                case 0:
                    //FEEDBACK: kako se ovo cekanje izvrsava u svakom slucaju prebaciti to van switch naredbe
                    //          ili odvojiti ovu logiku u metodu, kako se ponavlja kod samo s drugim prefabovima
                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    _leftLeftCannonPrefab = Instantiate(_leftLeftCannon, PosToSpawnLeftLeft, Quaternion.identity);
                    _rightLeftCannonPrefab = Instantiate(_rightLeftCannon, PosToSpawnRightLeft, Quaternion.identity);

                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    Destroy(_leftLeftCannonPrefab);
                    Destroy(_rightLeftCannonPrefab);
                    
                    break;
                case 1:
                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    _leftRightCannonPrefab = Instantiate(_leftRightCannon, PosToSpawnLeftRight, Quaternion.identity);
                    _rightLeftCannonPrefab = Instantiate(_rightLeftCannon, PosToSpawnRightLeft, Quaternion.identity);

                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    Destroy(_leftRightCannonPrefab);
                    Destroy(_rightLeftCannonPrefab);
                    break;
                case 2:
                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    _rightLeftCannonPrefab = Instantiate(_rightLeftCannon, PosToSpawnRightLeft, Quaternion.identity);
                    _rightRightCannonPrefab = Instantiate(_rightRightcannon, PosToSpawnRightRight, Quaternion.identity);

                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    Destroy(_rightLeftCannonPrefab);
                    Destroy(_rightRightCannonPrefab);
                    break;
                case 3:
                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    _rightRightCannonPrefab = Instantiate(_rightRightcannon, PosToSpawnRightRight, Quaternion.identity);
                    _leftRightCannonPrefab = Instantiate(_leftRightCannon, PosToSpawnLeftRight, Quaternion.identity);

                    yield return new WaitForSeconds(RandomTimeWaitCannon);
                    Destroy(_leftRightCannonPrefab);
                    Destroy(_rightRightCannonPrefab);
                    break;
                default:
                    break;
            }
            }
        }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _numberOfHits++;
        } 
    }

}

    

