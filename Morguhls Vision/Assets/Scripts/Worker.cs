using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    Animator anim;
    float maxHP = 30;
    float currentHP;
    Drain_Life dl = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (dl.IsDraining)
        {
            anim.SetBool("Scared", true);
        }
        if (currentHP <= 0)
        {
            anim.SetBool("Dead", true);
        }

    }
}
