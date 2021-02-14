using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // kada radimo sa scenama

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            SceneManager.LoadScene(1); // current scene load, po broju indeksa
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
