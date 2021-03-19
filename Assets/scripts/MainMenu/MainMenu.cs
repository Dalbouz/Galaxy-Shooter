using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Animator _mainMenuAnimator;
    private Animator _controlsPanelAnimator;

    private void Start()
    {
        _mainMenuAnimator = GameObject.Find("MainMenu").GetComponent<Animator>();
        if (_mainMenuAnimator == null)
            Debug.LogError("MainMenuAnimator je NULL");
        _controlsPanelAnimator = GameObject.Find("ControlsPanel").GetComponent<Animator>();
        if (_controlsPanelAnimator == null)
            Debug.LogError("Controls Panel Animator je jednak NULL");
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadControlPanel()
    {
        _mainMenuAnimator.SetBool("IsControlsPanel", true);
        _controlsPanelAnimator.SetBool("IsControlsPanel", true);
    }
    public void BackToMainMenuFromControlPanel()
    {
        _mainMenuAnimator.SetBool("IsControlsPanel", false);
        _controlsPanelAnimator.SetBool("IsControlsPanel", false);
    }
}
