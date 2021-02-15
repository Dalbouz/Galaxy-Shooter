using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;



    [SerializeField]
    private float _startSpeed = 4.5f;
    private float _staticSpeed = 4.5f;
    private float _newSpeedPowerUpSpeed = 8f;
    //FEEDBACK: brisati metode koje se ne koriste ili su prazne
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _horizontal * _startSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * _vertical * _startSpeed * Time.deltaTime);

        if (transform.position.y >= 5)
            transform.position = new Vector3(transform.position.x, 5, 0);
         else if (transform.position.y <= -3.8f)
            transform.position = new Vector3(transform.position.x, -3.8f, 0);

        if (transform.position.x > 9.3f)
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        else if (transform.position.x < -9.4f)
            transform.position = new Vector3(-9.4f, transform.position.y, 0);

    }

    IEnumerator CollectedSpeedBoost()
    {

            _startSpeed = _newSpeedPowerUpSpeed;
            yield return new WaitForSeconds(5);

             _startSpeed = _staticSpeed;
    }

    public void CallSpeedBoost()
    {
        StartCoroutine(CollectedSpeedBoost());
    }
}
