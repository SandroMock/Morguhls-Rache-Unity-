using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadGameScene : MonoBehaviour
{
    public string sceneName;
    public VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        //vp.loopPointReached += LoadScene;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneName);
    }
}
