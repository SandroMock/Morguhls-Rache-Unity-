using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    [SerializeField] AudioSource Dessert;
    [SerializeField] AudioSource Forest;
    [SerializeField] AudioSource Battle;
    bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        Dessert.Play();
        Forest.Play();
        Battle.Play();
        Forest.volume = 0;
        Battle.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            Dessert.volume -= Time.deltaTime * .5f ;
            Forest.volume += Time.deltaTime * .5f;
        }
        else
        {
            Dessert.volume += Time.deltaTime * .5f ;
            Forest.volume -= Time.deltaTime * .5f;
        }
        if (Movement.isBattle)
        {
            Forest.volume -= Time.deltaTime * .5f;
            Battle.volume += Time.deltaTime * 0.5f;
        }
        else if(!Movement.isBattle && isTriggered)
        {
            Forest.volume += Time.deltaTime * .5f;
            Battle.volume -= Time.deltaTime * 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
        }
        else if(other.tag == "Player" && isTriggered)
        {
            isTriggered = false;
        }
    }
}
