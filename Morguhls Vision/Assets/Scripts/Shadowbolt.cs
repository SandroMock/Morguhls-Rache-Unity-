using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Shadowbolt : MonoBehaviour
{
    float speed = 10;
    Transform enemy;
    private void Start()
    {
        enemy = FindObjectOfType<Player_Attack>().GetComponent<Player_Attack>().hit.transform;
    }
    void Update()
    {
        if(enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Worker" || collision.gameObject.tag == "WaveEnemy")
        {
            collision.gameObject.GetComponent<Enemy_Behavior>().GetDamage(10);
            Destroy(gameObject);
        }
    }
}
