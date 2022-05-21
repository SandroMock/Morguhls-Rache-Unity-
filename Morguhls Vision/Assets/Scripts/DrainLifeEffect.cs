using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainLifeEffect : MonoBehaviour
{
    Transform player;
    float speed = 20;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
