using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    PlayerHealth ph;
    [SerializeField] GameObject[] DeactivateParticles;
    // Start is called before the first frame update
    void Start()
    {
        ph = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(Vector3.Distance(transform.position, ph.transform.position) < 5)
        {
            DeactivateParticles[0].SetActive(false);
            DeactivateParticles[1].SetActive(false);
            ph.CurrentHealth = ph.MaxHealth;
            Destroy(GetComponent<SphereCollider>());
        }
    }
}
