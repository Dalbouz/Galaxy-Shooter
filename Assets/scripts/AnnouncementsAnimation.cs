using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementsAnimation : MonoBehaviour
{
    [SerializeField]
    private float _maxScale = 2.5f;
    [SerializeField]
    private float _growFactor = 0.15f;
    [SerializeField]
    private float _waitTime = 0.15f;
    void Start()
    {
    }

    private void Update()
    {
       
           
    }
public void ScaleAnim()
    {
        StartCoroutine(Scale());
    }
    IEnumerator Scale()
    {
        float timer = 0;
        while (true)
        {
            while(_maxScale > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * _growFactor;
                yield return null;
            }
            yield return new WaitForSeconds(_waitTime);
            timer = 0;
            while (1 < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * _growFactor;
                yield return null;
            }
            timer = 0;
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
