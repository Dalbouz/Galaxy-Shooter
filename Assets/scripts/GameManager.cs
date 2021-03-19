using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // kada radimo sa scenama

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;


    
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("UI Manager je jedna null");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            SceneManager.LoadScene(1); // current scene load, po broju indeksa
        if (Input.GetKey("escape"))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Time.timeScale = 0; kontrola vremena u igri (ako je 0, vrijeme ne prolazi tj. stoji, ako je 1 vrijeme prolazi normalno, ako je 0.5 vrijeme je usporeno za 2x ako je 2 vrijeme je ubrzano za 2 puta)
            _uiManager.PauseGame();
        }

        
    }
    public void GameOver()
    {
        _isGameOver = true;

    }

 

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
}
