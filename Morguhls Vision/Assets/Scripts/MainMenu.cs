using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject CinematicMenu;
    public string sceneName;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void Hover()
    {
        audio.PlayOneShot(hover);
    }
    public void Click()
    {
        audio.PlayOneShot(click);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Settings()
    {
        MenuPanel.SetActive(false);
        CinematicMenu.SetActive(true);
    }
    public void Credits()
    {

    }
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void BackToMenu()
    {
        CinematicMenu.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void Intro()
    {
        SceneManager.LoadScene("Cinematic");
    }
}
