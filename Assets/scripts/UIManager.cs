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
  
    
    

    

    
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    


    // Update is called once per frame
    public void UpdateScore(float newScore)// prima vrijednost iz PlayerScripte
    {
        _scoreText.text = "Score: " + newScore;
    }

    public void DisplayLives(int CurrentLife) // metoda koja se poziva u Player skripti prima vrijednost trenutnog zivota
    {
       _livesImg.sprite = _livesSprite[CurrentLife];//mjenjanje trenutne slike tj. sprajta, kojeg uzimamo iz liste ovisno o tome koliko zivota imamo
        //zato smo u Unityu spremili sprajtove tako da pod rednim brojem 3 je slika sa 3 zivota, rednim brojem 2 slika sa 2 zivota itd...
    }
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


