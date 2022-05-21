using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using EPOOutline;
using UnityEngine.Audio;

enum TYPES { DEFAULT, WORKER, BANDITS, VILLAGE_GUARDS, WAVE_KNIGHTS, HEAVY_KNIGHTS, HIGH_PRIEST};

public class Enemy_Behavior : MonoBehaviour
{
    int maxHealth;
    int resistence = 5;
    Animator anim;
    NavMeshAgent agent;
    [SerializeField] TYPES type = TYPES.DEFAULT;
    [SerializeField] Sprite[] sprite;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Image image;
    Time_Stop ts;
    Outlinable[] outlineToUse;
    bool isScared = false;
    AudioSource audio;
    [SerializeField] AudioClip clip;
    bool soundPlayed = false;
    GameObject Player;
    [SerializeField] Image Healthbar;
    [SerializeField] GameObject destroyHealthbar;



    public Vector3 StartPos { get; set; }

    private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gamemanager.GM.enemyTransform.Add(this.transform);
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (type == TYPES.WORKER)
        {
            maxHealth = 30;
            CurrentHealth = maxHealth;
        }
        else if(type == TYPES.BANDITS)
        {
            maxHealth = 70;
            currentHealth = maxHealth;
        }
        else if(type == TYPES.WAVE_KNIGHTS)
        {
            maxHealth = 100;
            currentHealth = maxHealth;
        }
        else if(type == TYPES.HEAVY_KNIGHTS)
        {
            maxHealth = 150;
            currentHealth = maxHealth;
        }
        else if(type == TYPES.VILLAGE_GUARDS)
        {
            maxHealth = 100;
            currentHealth = maxHealth;
            Gamemanager.GM.villageGuardsTransform.Add(this.transform);
        }
        sr.sprite = sprite[0];
        image.sprite = sprite[0];
        StartPos = transform.position;
        outlineToUse = GetComponentsInChildren<Outlinable>();
        foreach (var r in outlineToUse)
        {
            if(type == TYPES.WORKER)
            {
                r.OutlineParameters.Color = Color.yellow;
            }
            else
            {
                r.OutlineParameters.Color = Color.red;
            }
            r.OutlineParameters.Enabled = false;
        }
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(CurrentHealth != 30)
        //{
        //    isScared = true;
        //}
        //if ()
        //{
        //    isScared = true;
        //}
        if (type == TYPES.WORKER && isScared)
        {
            anim.SetBool("Scared", true);
        }
        if (CurrentHealth <= 0)
        {
            OnDeath();
        }


        if (ts.IsStopped)
        {
            if(CurrentHealth > 0)
            {
                agent.isStopped = true;
                anim.speed = 0;
            }
        }
        else
        {
            if (CurrentHealth > 0)
            {
                agent.isStopped = false;
                anim.speed = 1;
            }
        }

        if(type == TYPES.WAVE_KNIGHTS)
        {
            if (agent.enabled)
            {
                agent.SetDestination(Player.transform.position);
            }
        }
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= Mathf.Clamp(damage, 0, float.MaxValue);
        Healthbar.fillAmount = currentHealth / maxHealth;

    }
    void OnDeath()
    {
        if (!soundPlayed)
        {
            audio.PlayOneShot(clip, 0.25f);
            soundPlayed = true;
        }
        if(type == TYPES.VILLAGE_GUARDS)
        {
            Gamemanager.GM.villageGuardsTransform.Remove(this.transform);
        }
        Destroy(GetComponent<CapsuleCollider>());
        agent.enabled = false;
        anim.Play("Death");
        gameObject.tag = "Corpse";
        sr.sprite = sprite[1];
        image.sprite = sprite[1];
        Gamemanager.GM.enemyTransform.Remove(this.transform);
        Destroy(destroyHealthbar);
    }
    private void OnMouseEnter()
    {
        foreach(var r in outlineToUse)
        {
            r.OutlineParameters.Enabled = true;
        }
    }
    private void OnMouseExit()
    {
        foreach(var r in outlineToUse)
        {
            r.OutlineParameters.Enabled = false;
        }
    }
}
