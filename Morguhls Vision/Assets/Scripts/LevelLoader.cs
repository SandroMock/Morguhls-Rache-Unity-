using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class LevelLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider slider;
    public Text percentText;
    public Text ContinueText;
    public bool buttonPressed = false;
    public string sceneName;
    AsyncOperation operation;
    public VideoPlayer vp;

    public void Start()
    {
        vp.loopPointReached += LoadScene;

    }
    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(LoadAsync(sceneName));
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && LoadingScreen.activeInHierarchy)
        {
            buttonPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            vp.Stop();
            StopAllCoroutines();
            StartCoroutine(LoadAsync(sceneName));
        }
    }

    IEnumerator LoadAsync(string scenename)
    {
        yield return new WaitForSeconds(.1f);
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            percentText.text = progress * 100f + "%";
            if (operation.progress >= .9f)
            {
                percentText.gameObject.SetActive(false);
                ContinueText.gameObject.SetActive(true);
                yield return new WaitWhile(() => !buttonPressed);
                operation.allowSceneActivation = true;
            }
        }
    }
}
