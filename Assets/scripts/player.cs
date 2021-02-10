using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //stavljnje SerializeField iznad definiranje privatne varijable, mozemo interaktirati u Unityu iako je varijabla privatna
    [SerializeField]
    private float _startSpeed = 4.5f;//pocetna brzina
    private float _staticSpeed = 4.5f;//brzina koja se nikada nece promjenit
    private float _powerUpSpeed = 8f;//powerUP speed
    private bool _isSpeedPowerUpCooldown = true;

    private float _horizontal;
    private float _vertical;

    

    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;

    
    
    

    private float _offsetPrefabLaserY = 0.8f;
    private float _offsetPrefabLaserX = 0.79f;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _life = 3;

    private SpawnManager _spawnManager; //napravimo varijablu koja je tip "SpawnManager" tj. ona je tip skripte s kojom zelimo komunicirat, na ovaj nacin cemo dohvacat stvari iz SpawnManager skripte
  

    
    private bool _isTrippleShotActive = false;
    [SerializeField]
    private float _startNumberOfTrippleShots = 5;
    
    public bool IsShieldActive = false;
   // private float _currentNumberOfTrippleShots;
  //  private float _powerUpCounter = 5;
    
    void Start()
    {
        //take the current position = new position(0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        //tu u Startu odmah na pocetku spremamo komponentu "skriptu" od SpawnManagera u Varijablu i odmah sa Null provjeravamo da li ona postoji, tj. da li je uspjesno dohvacena ako nije javit ce nam da nepostoji tj. jednako null
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();// preko GameObject.Find("upisemo tocno kako se zove obj na kojem je ta skripta"), i sa get component dohvatiumo tu skirptu
        if (_spawnManager == null)
            Debug.LogError("Spawn Manager je jedna Null.");

        

    }


    void Update()
    {
        CalculateMovement();

        // Ako roknem Space i ako je Time.time(vrijeme koliko dugo je igra pokrenuta) veče od CanFire (-1), pocet cu spawnat GameObject
        //npr: Time.time=5, da li je veci od -1, je, izvršava se
        //npr: DRUGI KRUG - Time.time= 5, da li je to vece od 5.5, nije, nece se izvrsiti, cekamo 0,5 sekundi, kako bi Time.time bio 5.6, onda ce biti veći od CanFire(5.5)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        
    }

    void CalculateMovement()
    {
        //spremanje inputa Axis po horitontali i vertikali u privatne varijable
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        //new Vector3(1, 0, 0) * Axis po kojem se micem * speed * real time 
        transform.Translate(Vector3.right * _horizontal * _startSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * _vertical * _startSpeed * Time.deltaTime);

        /* ili ovako sa jednom linijom koda koristeci NEW VECTOR3
        _direction = new Vector3(_horizontal, _vertical, 0);
        transform.Translate(new Vector3(_horizontal, _vertical, 0) * _speed * Time.deltaTime);
        */

        /*Constrain movement:
         * ako je Player position na Y osi veci od 5
          Y position = 0 
        else ako je position na Y osi manja od -3,8f
        Y position = -3.8f
        */
        if (transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0);
        }
        else if (transform.position.y <= -3.8f)
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
      

        // ili mozemo to napraviti preko Mathf.Clamp naredbe = Mathf.Clamp(OS, Min, Max)
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5), 0);

        /*Kada izađe iz camera View po X osi, napravi krug i stvori se na -x Osi i obrnuto
         * ako je Player position na X osi veci od 12
         * X position = -11
         * ako je Player position na X osi manje od -12
         * X position = 11
         * */
        if (transform.position.x > 9.3f)
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        else if (transform.position.x < -9.4f)
            transform.position = new Vector3(-9.4f, transform.position.y, 0);

    }


   void FireLaser()
    {
        
            //povecavamo CanFire = vremenu trajanja igre + FireRate(0,5)
            //npr:CanFire=5 + 0,5 = 5.5
            _canFire = Time.time + _fireRate;

        if (_isTrippleShotActive == true && _startNumberOfTrippleShots >= 1) //ako je varijabla _isTrippleShotActive jednaka true i broj ispaljenih tripplehotova je veci od 0
        {
            Instantiate(_trippleShotPrefab, new Vector3(transform.position.x - _offsetPrefabLaserX, transform.position.y, transform.position.z), Quaternion.identity); //ispaljujemo trippleshot
            _startNumberOfTrippleShots--;
            if (_startNumberOfTrippleShots == 0) //ako je broj trippleshotova jedna nula, resetamo broj trippleshotova na pocetnu vrijednost, ali prebacujemo _isTrippleshotActive u False, tako da moramo ponovno pokupiti powerup
            {
                _startNumberOfTrippleShots = 5f;
                _isTrippleShotActive = false;
            }


        }
        else
            Instantiate(_laserprefab, new Vector3(transform.position.x, transform.position.y + _offsetPrefabLaserY, transform.position.z), Quaternion.identity);  
    }
   

    public void Damage()
    {
        if (IsShieldActive == true) // ako je shield aktivan
        {
            _shieldVisualizer.SetActive(false); // postavljamo vidljivost od shield na disabled tj. nevidljiv
            IsShieldActive = false; //postavit vrijednost da li je aktivan shield na False i cekat sljedeci put dok player ne pokupi shield power up 
            return; // return nas izbacuje odmah iz metoda DAMAGE, i nece izvršiti ništa što je napisano ispod return naredbe
        } 
        _life -= 1;
        
        if (_life == 0)//provjerit da li smo mrtvi
        {
            _spawnManager.OnPlayerDeath(); // ovdje smo pozvali metodu koja se nalazi unutar SpawnManager skripte
            Destroy(this.gameObject);
        }
    }

    

    public void TrippleShotActive() 
    {
        _isTrippleShotActive = true;
    }

    public void ShieldActive() // ukljucivanje Shield
    {
        IsShieldActive = true;
        _shieldVisualizer.SetActive(true); //Postavljamo njegovu vidljivost u Sceni na visible
    }

  
            
    
    IEnumerator SpeedPowerUp() //promjenimo speed cekamo 5 sekundi i onda napravimo break iz while petlje, i vratimo speed na pocetni speed
    {
        
        _startSpeed = _powerUpSpeed;
        
            
        
        yield return new WaitForSeconds(5);
        _startSpeed = _staticSpeed;
       
    }

    public void StartPowerUpSpeed() // ovo je samo metoda koja poziva tu korutinu, i ovu metodu pozivamo unutar PowerUp skripte
    {
        StartCoroutine(SpeedPowerUp());
    }

    

  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedPowerUp")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

}
