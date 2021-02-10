using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private float _yPosition=6.5f;
    private float _xPosition;

    private bool _destroyOnPlayerDeath = false;
    
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        //Pomicanje preda dolje 4 m po sekundi
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        RespawnWhenOnEnd();
        
    }

    //kada dolazi do kolizije (other nam vraca informacije o drugom objektu s kojim smo dosli do kolizije)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ako je tag od other jedna player, unistavamo Enemy
        if (other.tag == "Player")
        {

            Destroy(GameObject.FindWithTag("Enemy"));

            /*pozivamo damage metodu iz skripte player-->
             * other=sadrzi podatke o objektu s kojim se dogodila kolizija
             * transform=uvijek prvo ulazimo u root od objekta a to je transform
             * GetComponent<player>() = i sada dohvacamo komponentu(skriptu) koja se nalazi na objektu s kojim se dogodila kolizija i skripta se zove player
             * da nismo koristili null referencu(provjeravali da li postoji ta komponenta), mogli smo npr. dohvatit Damage metodu ovako
             * other.transform.GetComponent<player>().Damage();
             */
            player DamageScript = other.transform.GetComponent<player>();
            //provjeravamo da li ova skripta postoji na našem "other" objektu, ako da izvrši je 
            if (DamageScript != null)
                DamageScript.Damage();
        }


        // ako je tag od other jednak Laseru, unistavamo prvo laser a onda Enemy
        if (other.tag == "Laser")
        {
            // Mozemo i ovako preko FindWithTag
            //Destroy(GameObject.FindWithTag("Laser"));
            // Destroy(GameObject.FindWithTag("Enemy"));

            //ili ovako, other oznacava game objekt s kojim smo dosli u koliziju, a this oznacava gameobjekt na kojeg je stavljena ova skripta
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void RespawnWhenOnEnd() 
    {
        // ako objekt dojde do kraja ekrana isti taj objekt ce se spawnat na vrh ekrana, i ako dojde do kraja ekrana spawnat ce se negdje na radnom po X osi 
        if (transform.position.y <= -4.5f && _destroyOnPlayerDeath == false)
        {
            _xPosition = Random.Range(-9.6f, 9.6f);
            transform.position = new Vector3(_xPosition, _yPosition, 0);
        }
        

    }
    
}
