using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampTrigger : MonoBehaviour
{
    [SerializeField] GameObject text;
    float textTimer = 4f;
    bool isText = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            text.SetActive(true);
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
