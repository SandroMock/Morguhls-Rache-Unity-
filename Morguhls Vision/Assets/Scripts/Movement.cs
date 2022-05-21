using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    Ray ray = default;
    RaycastHit hit = default;
    NavMeshAgent agent = null;
    Animator anim = null;
    PlayerHealth playerHP;
    Time_Stop ts;
    //[SerializeField] GameObject marker;
    [SerializeField] float speed;
    bool isReachable = false;
    float checkTimer = .1f;
    bool isSceletonHit = false;
    public static bool isBattle = false;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = GetComponent<PlayerHealth>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerHP = GetComponent<PlayerHealth>();
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gamemanager.GM.isStart) return;
        if (Input.GetMouseButton(0) && playerHP.CurrentHealth > 0 && !ts.IsStopped)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.transform.CompareTag("Enemy") || !hit.transform.CompareTag("WaveEnemy") || !hit.transform.CompareTag("Sceleton") || !hit.transform.CompareTag("Worker"))
                {
                    isReachable = true;
                    anim.SetBool("Walk", true);
                    agent.SetDestination(hit.point);
                    //marker.SetActive(true);
                    //marker.transform.position = hit.point + new Vector3(0,.01f,0);
                }
            }
        }
        if (isReachable)
        {
            checkTimer -= Time.deltaTime;
            if(checkTimer <= 0)
            {
                if(agent.velocity == Vector3.zero)
                {
                    anim.SetBool("Walk", false);
                    //marker.SetActive(false);
                }
                checkTimer = .1f;
            }
        }
        if (ts.IsStopped)
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            agent.isStopped = true;
        }
        else if(!ts.IsStopped && !isSceletonHit)
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            agent.isStopped = false;
        }
        if (agent.remainingDistance <= .1f && agent.hasPath)
        {
            anim.SetBool("Walk", false);
            //marker.SetActive(false);
        }
        if (Raise_Death.isActive)
        {
            anim.SetBool("Raise", true);
        }
        else
        {
            anim.SetBool("Raise", false);
        }
        if (ts.IsStopped)
        {
            anim.SetBool("IsStop", true);
        }
        else
        {
            anim.SetBool("IsStop", false);
        }

        //if(Vector3.Distance(transform.position, GameObject.FindGameObjectsWithTag("Enemy")) <= 10)
        //{
        //    isBattle = true;
        //}
        //else
        //{
        //    isBattle = false;
        //}
        //marker.transform.Rotate(new Vector3(0,0,1) * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sceleton")
        {
            isSceletonHit = true;
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Sceleton")
        {
            isSceletonHit = false;
        }
    }
}
