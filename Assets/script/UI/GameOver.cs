using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private GameObject GameOverCanvas;

    public void ShowGameOverCanvas()
    {
        HUDCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
    }

    public void ReStartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
