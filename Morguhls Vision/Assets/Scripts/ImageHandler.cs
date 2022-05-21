using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    [SerializeField] Sprite[] sprite;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Image image;
    Time_Stop ts;
    // Start is called before the first frame update
    void Start()
    {
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ts.IsStopped)
        {
            sr.sprite = sprite[0];
            image.sprite = sprite[0];
        }
    }
}
