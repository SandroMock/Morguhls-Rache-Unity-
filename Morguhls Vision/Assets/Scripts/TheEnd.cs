using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TheEnd : MonoBehaviour
{
    [SerializeField] VideoPlayer vp;
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        vp.loopPointReached += Panel;
    }

    void Panel(VideoPlayer vp)
    {
        panel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            vp.Stop();
            panel.SetActive(true);
        }
    }
}
