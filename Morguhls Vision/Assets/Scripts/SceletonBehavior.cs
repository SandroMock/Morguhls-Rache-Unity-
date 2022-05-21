using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using EPOOutline;
using UnityEngine.Audio;

public class SceletonBehavior : MonoBehaviour
{
    GameObject player = null;
    NavMeshAgent agent = null;
    Animator anim = null;
    Time_Stop ts;
    Vector3 startPos;
    Outlinable[] outlineToUse;
    bool isOverGO = false;
    [SerializeField] GameObject pin;
    [SerializeField] GameObject destroyHealthbar;
    float maxHealth = 100;
    [SerializeField] AudioSource audio;

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    [SerializeField] Image healthBar;
    private float currentHealth;
    
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    [SerializeField] Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
        Gamemanager.GM.sceletonTransform.Add(this.transform);
        Gamemanager.GM.enemyAttackTransform.Add(this.transform);
        currentHealth = maxHealth;
        healthBar.color = gradient.Evaluate(1f);
        outlineToUse = GetComponentsInChildren<Outlinable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (ts.IsStopped)
            {
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                agent.isStopped = true;
                anim.speed = 0;
                GetComponent<CapsuleCollider>().height = 2;
                //foreach (var r in outlineToUse)
                //{
                //    r.OutlineParameters.Enabled = true;
                //}
            }
            if (!ts.IsStopped)
            {
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                anim.speed = 1;
                GetComponent<CapsuleCollider>().height = 0;
                //agent.isStopped = false;
                foreach (var r in outlineToUse)
                {
                    r.OutlineParameters.Enabled = false;
                }
            }
        }
        if (CurrentHealth >= maxHealth) currentHealth = maxHealth;
        if(CurrentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= Mathf.Clamp(damage, 0, float.MaxValue);
        healthBar.fillAmount = currentHealth / maxHealth;
        healthBar.color = gradient.Evaluate(healthBar.fillAmount);
    }

    void OnDeath()
    {
        anim.Play("Death");
        Gamemanager.GM.sceletonTransform.Remove(this.transform);
        Gamemanager.GM.enemyAttackTransform.Remove(this.transform);
        Destroy(GetComponent<CapsuleCollider>());
        pin.SetActive(false);
        agent.enabled = false;
        Destroy(destroyHealthbar);
    }

    private void OnMouseDown()
    {
        audio.Play();
        if (ts.IsStopped)
        {
            startPos = transform.position;
            //coll.isTrigger = true;
            Debug.Log("StartPos " + startPos);
        }
    }
    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && ts.IsStopped)
        {
            transform.position = hit.point;
            if(Vector3.Distance(startPos, transform.position) > 5)
            {
                foreach (var r in outlineToUse)
                {
                    r.OutlineParameters.Color = Color.red;
                }
            }
            else
            {
                foreach(var r in outlineToUse)
                {
                    r.OutlineParameters.Color = Color.green;
                }
            }
            if (isOverGO)
            {
                foreach (var r in outlineToUse)
                {
                    r.OutlineParameters.Color = Color.red;
                }
            }
        }
        foreach(var r in outlineToUse)
        {
            if (!ts.IsStopped && r.OutlineParameters.Color == Color.red)
            {
                transform.position = startPos;
            }
        }
    }
    private void OnMouseUp()
    {
        foreach (var r in outlineToUse)
        {
            if(r.OutlineParameters.Color == Color.red)
            {
                transform.position = startPos;
                r.OutlineParameters.Color = Color.white;
            }
        }
    }
    private void OnMouseEnter()
    {
        if (ts.IsStopped)
        {
            foreach(var r in outlineToUse)
            {
                r.OutlineParameters.Enabled = true;
                r.OutlineParameters.Color = Color.yellow;
                //r.OutlineParameters.Enabled = true;
            }
        }
    }
    private void OnMouseExit()
    {
        if (ts.IsStopped)
        {
            foreach(var r in outlineToUse)
            {
                r.OutlineParameters.Enabled = false;
                r.OutlineParameters.Color = Color.white;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ts.IsStopped)
        {
            isOverGO = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ts.IsStopped)
        {
            isOverGO = false;
        }
    }
}
