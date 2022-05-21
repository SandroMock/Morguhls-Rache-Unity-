using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager GM;
    public bool isStart = false;
    public List<Transform> sceletonTransform = new List<Transform>();
    public List<Transform> enemyTransform = new List<Transform>();
    public List<Transform> enemyAttackTransform = new List<Transform>();
    public List<Transform> villageGuardsTransform = new List<Transform>();


    private void Awake()
    {
        if(GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(8);
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isStart)
        //{
        //    sceletonAmmount.text = sceletonTransform.Count.ToString();
        //}
    }

    public Transform GetNearestEnemy(Vector3 ownPos, float maxDist = -1)
    {
        if (maxDist < 0) maxDist = 15;
        Transform enemy = null;
        float actualDist = float.MaxValue;
        foreach(Transform t in enemyTransform)
        {
            if(Vector3.Distance(ownPos, t.position) < actualDist)
            {
                enemy = t;
                actualDist = Vector3.Distance(ownPos, t.position);
            }
        }
        if(actualDist > maxDist)
        {
            enemy = null;
        }
        return enemy;
    }

    public Transform GetNearestSceleton(Vector3 ownPos, float maxDist = -1)
    {
        if (maxDist < 0) maxDist = 25;
        Transform sceleton = null;
        float actualDist = float.MaxValue;
        foreach (Transform t in enemyAttackTransform)
        {
            if (Vector3.Distance(ownPos, t.position) < actualDist)
            {
                sceleton = t;
                actualDist = Vector3.Distance(ownPos, t.position);
            }
        }
        if (actualDist > maxDist)
        {
            sceleton = null;
        }
        return sceleton;
    }
}
