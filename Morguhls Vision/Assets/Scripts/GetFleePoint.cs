using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetFleePoint : MonoBehaviour
{
    [SerializeField] GameObject FleePoint;
    NavMeshAgent agent;
    HighPriestTrigger hpt;
    float timer = 3;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hpt = FindObjectOfType<HighPriestTrigger>().GetComponent<HighPriestTrigger>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hpt.IsKing)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                agent.SetDestination(FleePoint.transform.position);
                if(Vector3.Distance(transform.position, FleePoint.transform.position) <= 1)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
