using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;

    public void Hover()
    {
        audio.PlayOneShot(hover);
    }
    public void Click()
    {
        audio.PlayOneShot(click);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
