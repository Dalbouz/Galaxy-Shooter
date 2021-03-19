using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //kada god nesto radimo sa UI trebamo ovo implementirati


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText; //kada implementiramo "using UnityEngine.ui" mozemo dohvacati njegove komponente ovako | u unityu dodamo objekt TEXT u skriptu

    [SerializeField]
    private Image _livesImg; //dodajemo varijablu koja je tipa "image" u nju mozemo spremati current sprite tj. trenutnu sliku | dohvacamo Source image koja se nalazi u objektu Lives_display
    [SerializeField]
    private Sprite[] _livesSprite;// u ovu listu spremamo vise images sprietova koje cemo kasnije izmjenjivati
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevelText;
    [SerializeField]
    private Button _joinStart;
    [SerializeField]
    private Button _dontJoinStartButton;
    // [SerializeField]
    // private Image[] _powerUpsSlot; // 0 = Shield, 1 = Speed, 2 = SpeedShot, 3 = TrippleShoot
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private GameObject _PowerUpAnnouncment;
    [SerializeField]
    private GameObject _laserCannonAnnouncment;
    [SerializeField]
    private GameObject _enemySpawnAnnouncment;
    
    
    private Animator _pauseAnimator;
    private AnnouncementsAnimation _announcementsAnimation;
    
    
    
    

    

    
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _pauseAnimator = GameObject.Find("PauseMenuPanel").GetComponent<Animator>();
        if (_pauseAnimator == null)
            Debug.LogError("PauseMenuPanel je jednak null");
        
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime; //sto znaci da ikakva interupcija sa vremenom nece zaustaviti izvođenje animacije ovog objekta
        StartCoroutine(ActivateButtonStartStory());
    }

    


    // Update is called once per frame
    public void UpdateScore(float newScore)// prima vrijednost iz PlayerScripte
    {
        _scoreText.text = "Score: " + newScore;
    }

  /*  public void DisplayLives(int CurrentLife) // metoda koja se poziva u Player skripti prima vrijednost trenutnog zivota
    {
       _livesImg.sprite = _livesSprite[CurrentLife];//mjenjanje trenutne slike tj. sprajta, kojeg uzimamo iz liste ovisno o tome koliko zivota imamo
        //zato smo u Unityu spremili sprajtove tako da pod rednim brojem 3 je slika sa 3 zivota, rednim brojem 2 slika sa 2 zivota itd...
    }*/
    IEnumerator OnOfGameOverText() //kada player umre palimo i gasimo GAME OVER TEXT
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            
        }
    }
    public void DisplayGameOverAndRestartLevelText()
    {
        _gameOverText.GetComponent<Text>().enabled = true;// ukljucivanje visibilty 
        StartCoroutine(OnOfGameOverText());
        _restartLevelText.GetComponent<Text>().enabled = true;
        
    }
  
    public void DisplayPowerUps(int PowerUp) //ukljucivanje visibility na slikama, ovisno koji powerUp player pokupi
    {
        switch (PowerUp)
        {
            case 0:
                GameObject.Find("TrippleShotPowerUp").GetComponent<Image>().enabled = true;
                break;
            case 1:
                GameObject.Find("SpeedPowerUp").GetComponent<Image>().enabled = true;
                break;
            case 2:
                GameObject.Find("ShieldPowerUp").GetComponent<Image>().enabled = true;
                break;
            case 3:
                GameObject.Find("SpeedShotPowerUp").GetComponent<Image>().enabled = true;
                break;
            default:
                break;
        }
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _pauseAnimator.SetBool("IsPause", true);
 
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator ActivateButtonStartStory()
    {
        yield return new WaitForSeconds(27f);
        _dontJoinStartButton.gameObject.SetActive(true);
        _joinStart.gameObject.SetActive(true);
        

    }

    public void TurnOnAsteroidAnnouncment(int Rand)
    {
        switch (Rand)
        {
            case 0:   
               _enemySpawnAnnouncment.SetActive(true);
                _announcementsAnimation = GameObject.Find("EnemySpawner").GetComponent<AnnouncementsAnimation>();
                _announcementsAnimation.ScaleAnim();
                StartCoroutine(TurnOffAsteroidAnnouncment());
                break;
            case 1:
                _laserCannonAnnouncment.SetActive(true);
                _announcementsAnimation = GameObject.Find("LaserCannon").GetComponent<AnnouncementsAnimation>();
                _announcementsAnimation.ScaleAnim();
                StartCoroutine(TurnOffAsteroidAnnouncment());
                break;
            case 2:
                _PowerUpAnnouncment.SetActive(true);
                _announcementsAnimation = GameObject.Find("PowerUp").GetComponent<AnnouncementsAnimation>();
                _announcementsAnimation.ScaleAnim();
                StartCoroutine(TurnOffAsteroidAnnouncment());
                break;
            default:
                break;
        }


    }
   
       IEnumerator TurnOffAsteroidAnnouncment()
    {
        yield return new WaitForSeconds(1.8f);
        _PowerUpAnnouncment.SetActive(false);
        _laserCannonAnnouncment.SetActive(false);
        _enemySpawnAnnouncment.SetActive(false);
    }
    
    
    
    
    
    /*public void ShowPowerUpRunning()
    {

        var StartPosition = GameObject.Find("ShieldPowerUp").GetComponent<RectTransform>();
    }*/

    

  /*  private void LoadSameLevel()
    {
        _stopflikerGameOverText = true;
        _gameOverText.GetComponent<Text>().enabled = false;
        _restartLevelText.GetComponent<Text>().enabled = false;
        _isGameOver = false;
        _stopflikerGameOverText = false;

        SceneManager.LoadScene("Level01", LoadSceneMode.Additive);
    }*/
}


