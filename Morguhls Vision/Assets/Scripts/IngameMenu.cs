using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject SettingsMenu;
    Time_Stop ts;
    bool isMenuActive = false;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ts.IsStopped && !SettingsMenu.activeInHierarchy)
        {
            MenuPanel.SetActive(!MenuPanel.activeSelf);
            //OpenMenu();
        }

        if (MenuPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void Settings()
    {
        MenuPanel.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void Resume()
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenMenu()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    public void BackToMenu()
    {
        SettingsMenu.SetActive(false);
        MenuPanel.SetActive(true);
    }


    public void Hover()
    {
        audio.PlayOneShot(hover);
    }
    public void Click()
    {
        audio.PlayOneShot(click);
    }
}
