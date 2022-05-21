using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditTrigger : MonoBehaviour
{
    [SerializeField] GameObject text;
    float textTimer = 3f;
    bool isText = false;
    public bool isAlert { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isAlert = true;
            if (textTimer > 0)
            {
                text.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isText = true;
        }
    }

    private void Update()
    {
        if (isText)
        {
            textTimer -= Time.deltaTime;
            if (textTimer <= 0)
            {
                text.SetActive(false);
            }
        }
    }
}
