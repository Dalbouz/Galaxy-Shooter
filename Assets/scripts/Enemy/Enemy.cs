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

    private player _player;
    private Animator _animator; // Kreiranje kontrolera za Animator komponentu
    private AudioSource _audioSource;
    
    
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<player>(); // puno bolje je ovako trazit komponentu u void Start, jer nam se sada ta komponenta spremila i mozemo ju koristiti dalje u programu
        if (_player == null)
            Debug.LogError("Player je jednak null.");
        _animator = gameObject.GetComponent<Animator>();// dohvacanje animator komponente 
        if (_animator == null)
            Debug.LogError("Animator je jednak null.");
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.LogError("Audio Source je jednak null.");
        
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


            /*pozivamo damage metodu iz skripte player-->
             * other=sadrzi podatke o objektu s kojim se dogodila kolizija
             * transform=uvijek prvo ulazimo u root od objekta a to je transform
             * GetComponent<player>() = i sada dohvacamo komponentu(skriptu) koja se nalazi na objektu s kojim se dogodila kolizija i skripta se zove player
             * da nismo koristili null referencu(provjeravali da li postoji ta komponenta), mogli smo npr. dohvatit Damage metodu ovako
             * other.transform.GetComponent<player>().Damage();
             */
            
            //provjeravamo da li ova skripta postoji na našem "other" objektu, ako da izvrši je 
            if (_player != null)
                _player.Damage();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; // iskljucujemo njegov box collider tako da se player nemoze zaletit u exploziju
            _animator.SetTrigger("IsDestroy");
            _audioSource.Play();
            _speed = 0;
            Destroy(this.gameObject,4.8f);
        }


        // ako je tag od other jednak Laseru, unistavamo prvo laser a onda Enemy
        if (other.tag == "Laser")
        {
            
            // Mozemo i ovako preko FindWithTag
            //Destroy(GameObject.FindWithTag("Laser"));
            // Destroy(GameObject.FindWithTag("Enemy"));

            //ili ovako, other oznacava game objekt s kojim smo dosli u koliziju, a this oznacava gameobjekt na kojeg je stavljena ova skripta
            Destroy(other.gameObject);
            _speed = 0;
            _animator.SetTrigger("IsDestroy");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2f);// ovih 4.8f je wait time, prvo cekamo određeno vrijeme i onda unistimo objekt
            if(_player!=null)
                _player.AddScore(Random.Range(5,50)); // tu pozivamo Addscore metodu i vraca vrijednost random izeđu 5 i 50, i spremat ce se kao varijabla points unutar Player scripte
            
            
        }
        
    }

    private void RespawnWhenOnEnd() 
    {
        // ako objekt dojde do kraja ekrana isti taj objekt ce se spawnat na vrh ekrana, i ako dojde do kraja ekrana spawnat ce se negdje na radnom po X osi 
        if (transform.position.y <= -4.5f && _destroyOnPlayerDeath == false)
        {
            _player.AddScore(-10);
            _xPosition = Random.Range(-9.6f, 9.6f);
            transform.position = new Vector3(_xPosition, _yPosition, 0);
        }
        

    }
    
    
   
}
