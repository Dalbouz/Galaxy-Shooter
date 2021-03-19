using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //stavljnje SerializeField iznad definiranje privatne varijable, mozemo interaktirati u Unityu iako je varijabla privatna
    [SerializeField]
    private float _startSpeed = 5.5f;//pocetna brzina
    private float _staticSpeed = 5.5f;//brzina koja se nikada nece promjenit
    private float _powerUpSpeed = 8f;//powerUP speed
    [SerializeField]
    private float _engineDownSpeed = 2;
    private Vector3 _newSpeedUpScale= new Vector3(0.3f,0,0);

   // private bool _isSpeedPowerUpCooldown = true;
    private float _horizontal;
    private float _vertical;

    private int _currentScore;

    [SerializeField]
    private int _enemyDestroyCounter;

    private int _numberofCollisionsWithEnemy = 0;
    

    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _thruster;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private AudioClip _laserShootSound; // u unityu drag&dropamo naš Clip
    [SerializeField]
    private AudioClip _engineDownSound;
   
    
    private AudioSource _audioSource; //varijabla za dohvacanje Audio Source komponente na Playeru
   

    

    private float _offsetPrefabLaserY = 0.8f;
    private float _offsetPrefabLaserX = 0.79f;

    //ovo koristimo kada pucamo na ONBUTTOW DOWN
    [SerializeField]
    private float _fireRate = 0.2f;
    public float speedFireRate = 0f;
    private float _canFire = -1f;

    [SerializeField]
    private int _scoreToSpawnBoss1 = 1200;
   

   /* 
    * Ako koristimo samo 3 Zivota i prikazujemo to sa slikama
    [SerializeField]
    private int _life = 3;
   */

    [SerializeField]
    private int _startHealth = 100;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _healUp = 40;
    [SerializeField]
    private int _engineStartHealth = 0;
    private int _engineCurrentHealth;



    private SpawnManager _spawnManager; //napravimo varijablu koja je tip "SpawnManager" tj. ona je tip skripte s kojom zelimo komunicirat, na ovaj nacin cemo dohvacat stvari iz SpawnManager skripte
    private UIManager _uiManager;
    private GameManager _gameManager;
    private HealthBar _healthBar;
    private EngineHealth _engineHealth;
    private Animator _animator;
    

    bool UpPressed = false;
    bool downPressed = false;
  

    
    private bool _isTrippleShotActive = false;
    [SerializeField]
    private float _startNumberOfTrippleShots = 15;

    private bool _isEngineDown = false;

    private bool _isEngineSoundOn = false;

    public bool IsSpeedBoostActive = false;
    public bool IsShieldActive = false;
   // private float _currentNumberOfTrippleShots;
  //  private float _powerUpCounter = 5;
    
    void Start()
    {

        


        //take the current position = new position(0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        //tu u Startu odmah na pocetku spremamo komponentu "skriptu" od SpawnManagera u Varijablu i odmah sa Null provjeravamo da li ona postoji, tj. da li je uspjesno dohvacena ako nije javit ce nam da nepostoji tj. jednako null
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();// preko GameObject.Find("upisemo tocno kako se zove obj na kojem je ta skripta"), i sa get component dohvatiumo tu skirptu
        if (_spawnManager == null) //provjeravamo odmah na pocetku da li smo uspjeli dohvatiti skriptu
            Debug.LogError("Spawn Manager je jednak NULL.");
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Animator je jednak Null.");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("UIManager je jednak NULL.");
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
            Debug.LogError("GameManager je jednak NULL");
        _healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        if (_healthBar == null)
            Debug.LogError("HealthBar je  jednak NULL.");
        _engineHealth = GameObject.Find("EngineHealth").GetComponent<EngineHealth>();
        if (_engineHealth == null)
            Debug.LogError("EngineHealth je jednak NULL.");
        _audioSource = GetComponent<AudioSource>(); //dohvatimo audiosource komponentu
        if (_audioSource == null)
            Debug.LogError("Audio Source on the player is NULL");
        else
            _audioSource.clip = _laserShootSound; // prije ovoga je slot u komponenti AUDIO SOURCE na playeru prazan, sa ovime dodjeljujemo audio clip
       // StartCoroutine(FireLaserAuto());
        
        _currentHealth = _startHealth;
        _healthBar.StartSlider(_currentHealth);
        _engineCurrentHealth = _engineStartHealth;
        _engineHealth.EngineStart(_engineCurrentHealth);
    }


    void Update()
    {
        CalculateMovement();

        // Ako roknem Space i ako je Time.time(vrijeme koliko dugo je igra pokrenuta) veče od CanFire (-1), pocet cu spawnat GameObject
        //npr: Time.time=5, da li je veci od -1, je, izvršava se
        //npr: DRUGI KRUG - Time.time= 5, da li je to vece od 5.5, nije, nece se izvrsiti, cekamo 0,5 sekundi, kako bi Time.time bio 5.6, onda ce biti veći od CanFire(5.5)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
         {
          FireLaserOnButtonDown();
         }

        
    }

    void CalculateMovement()
    {
        //spremanje inputa Axis po horitontali i vertikali u privatne varijable
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (_horizontal == -1)
            _animator.SetBool("IsLeft", true);
        else
            _animator.SetBool("IsLeft", false);
        if (_horizontal == 1)
            _animator.SetBool("IsRight", true);
        else
            _animator.SetBool("IsRight", false);
        
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
        else if (transform.position.y <= -3f)
            transform.position = new Vector3(transform.position.x, -3f, 0);
      

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

    /*IEnumerator FireLaserAuto()
    {
        while (true)
        {

        if (_isTrippleShotActive == true && _startNumberOfTrippleShots >= 1) //ako je varijabla _isTrippleShotActive jednaka true i broj ispaljenih tripplehotova je veci od 0
        {
            Instantiate(_trippleShotPrefab, new Vector3(transform.position.x - _offsetPrefabLaserX, transform.position.y, transform.position.z), Quaternion.identity); //ispaljujemo trippleshot
            _startNumberOfTrippleShots--;
            if (_startNumberOfTrippleShots == 0) //ako je broj trippleshotova jedna nula, resetamo broj trippleshotova na pocetnu vrijednost, ali prebacujemo _isTrippleshotActive u False, tako da moramo ponovno pokupiti powerup
            {
                _startNumberOfTrippleShots = 15f;
                _isTrippleShotActive = false;
            }
        }
        else
            Instantiate(_laserprefab, new Vector3(transform.position.x, transform.position.y + _offsetPrefabLaserY, transform.position.z), Quaternion.identity);
        _audioSource.Play(); // pokretanje CLIPA koji je na audioSource komponenti
            yield return new WaitForSeconds(0.4f);
        }
    }*/
   
     void FireLaserOnButtonDown()
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
                GameObject.Find("TrippleShotPowerUp").GetComponent<PowerUpUiImage>().TurnOff();
            }


        }
        else {
            
            Instantiate(_laserprefab, new Vector3(transform.position.x, transform.position.y + _offsetPrefabLaserY, transform.position.z), Quaternion.identity);
        }
        if(_isEngineSoundOn == false)
            _audioSource.Play();
        
        
    }

    
   

    public void Damage()
    {
        if (IsShieldActive == true) // ako je shield aktivan
        {
            _shieldVisualizer.SetActive(false); // postavljamo vidljivost od shield na disabled tj. nevidljiv
            IsShieldActive = false; //postavit vrijednost da li je aktivan shield na False i cekat sljedeci put dok player ne pokupi shield power up 
            GameObject.Find("ShieldPowerUp").GetComponent<PowerUpUiImage>().TurnOff();
            return; // return nas izbacuje odmah iz metoda DAMAGE, i nece izvršiti ništa što je napisano ispod return naredbe
        } 
       // _life -= 1; // ako imamo 3 zivota

        //HEALT CONTROL
        _currentHealth -= 20;
        _healthBar.SliderHealth(_currentHealth);
        
        //ENGINE CONTROL
        _audioSource.clip = _engineDownSound;
        _audioSource.loop = true;
        _isEngineSoundOn = true;
        _audioSource.volume = 0.04f;

        _audioSource.Play();
        _numberofCollisionsWithEnemy += 1;
        _isEngineDown = true;
        _engineCurrentHealth += 1;
        _engineHealth.EngineDamage(_engineCurrentHealth);
       if(_numberofCollisionsWithEnemy == 1)
            _spawnManager.SpawnRepair();
        if (_engineCurrentHealth == 1)
            _rightEngine.SetActive(true);
         if (_engineCurrentHealth == 2)
        {

            _leftEngine.SetActive(true);
            _startSpeed -= _engineDownSpeed;

        }
       // _uiManager.DisplayLives(_life); //pokretanje metode DisplayLives unutar UIManagera, prima vrijednost od _life 
        

        if (_currentHealth <= 0)//provjerit da li smo mrtvi
        {
            _spawnManager.OnPlayerDeath(); // ovdje smo pozvali metodu koja se nalazi unutar SpawnManager skripte
            Destroy(this.gameObject);
            _uiManager.DisplayGameOverAndRestartLevelText();
            _gameManager.GameOver();
            
            
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

  
            
    
    IEnumerator SpeedPowerUp() //promjenimo speed cekamo 5 sekundi  i vratimo speed na pocetni speed
    {

        IsSpeedBoostActive = true;
        _startSpeed = _powerUpSpeed;
        _thruster.transform.localScale += _newSpeedUpScale;
            
        
        yield return new WaitForSeconds(5);
        _thruster.transform.localScale -= _newSpeedUpScale;
        _startSpeed = _staticSpeed;
        GameObject.Find("SpeedPowerUp").GetComponent<PowerUpUiImage>().TurnOff();
        IsSpeedBoostActive = false;
       
    }

    public void RepairEngine()
    {
        _audioSource.clip = _laserShootSound;
        _audioSource.loop = false;
        _isEngineSoundOn = false;
        _audioSource.volume = 0.5f;
        _numberofCollisionsWithEnemy = 0;
        _spawnManager.EngineRepairedStopSpawning(); // da prestane spawnat engine repair power up
       if(_engineCurrentHealth >= 1)
            _engineCurrentHealth = _engineStartHealth;
        _engineHealth.HealEngine(_engineCurrentHealth);
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);
        _startSpeed = _staticSpeed;

        
    }

    public void HealUp()
    {
        if (_currentHealth == _startHealth)
            return;
        else if (_currentHealth == 80)
        {
            _currentHealth += 20;
            _healthBar.SliderHealth(_currentHealth);
        }
        else { 
        _currentHealth += _healUp;
        _healthBar.SliderHealth(_currentHealth);
        }
    }

    public void StartPowerUpSpeed() // ovo je samo metoda koja poziva tu korutinu, i ovu metodu pozivamo unutar PowerUp skripte
    {
        if (_engineCurrentHealth <= 1)
            StartCoroutine(SpeedPowerUp());
        else
            return;
    }


   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedPowerUp")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }*/

    IEnumerator SpeedShotRoutine()
    {
        _fireRate = speedFireRate;
        yield return new WaitForSeconds(10);
        GameObject.Find("SpeedShotPowerUp").GetComponent<PowerUpUiImage>().TurnOff();
        _fireRate = 0.2f;
    }

    public void StartSpeedShotRoutine()
    {
        
            StartCoroutine(SpeedShotRoutine());
        
    }

    
   
    
    public void AddScore(int points) // ova metoda prima 1 parametar tipa int koji se zove points(taj points nemoramo nigdje predefinirat jer ga definiramo sada tu) tu vrijednost prima iz EnemyScripte
    {
        _currentScore += points;
        _enemyDestroyCounter++;
        if (_enemyDestroyCounter == 150)
            _spawnManager.SpawnBoss1();
        _uiManager.UpdateScore(_currentScore);
    }


}
