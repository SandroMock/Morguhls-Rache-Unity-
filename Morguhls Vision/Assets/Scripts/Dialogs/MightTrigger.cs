using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightTrigger : MonoBehaviour
{
    [SerializeField] GameObject text;
    float textTimer = 3f;
    bool isText = false;
    public bool isAlert { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            text.SetActive(true);
            isAlert = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isText = true;
            Destroy(GetComponent<BoxCollider>());
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
