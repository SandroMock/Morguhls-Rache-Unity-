using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] GameObject shadowBolt;
    [SerializeField] GameObject startPos;
    Ray ray = default;
    public RaycastHit hit = default;
    [SerializeField] float fireRate = .25f;
    float canFire = 0;
    Animator anim;
    [SerializeField] TextMeshProUGUI rangeTxt;
    float showTextTimer = 2;
    bool isTextShown = false;
    bool isShadowBolt = true;
    Time_Stop ts;
    [SerializeField] AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && Time.time > canFire)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("WaveEnemy") || hit.transform.CompareTag("Worker"))
                {
                    if(!ts.IsStopped)
                    {
                        if(Vector3.Distance(transform.position, hit.transform.position) > 10)
                        {
                            rangeTxt.gameObject.SetActive(true);
                            isTextShown = true;
                        }
                        else
                        {
                            anim.SetTrigger("Attack");
                            transform.LookAt(hit.transform);
                            ShadowBolt();
                        }
                    }
                }
            }
        }
        if (isTextShown)
        {
            showTextTimer -= Time.deltaTime;
            if (showTextTimer <= 0)
            {
                rangeTxt.gameObject.SetActive(false);
                showTextTimer = 2;
                isTextShown = false;
            }
        }
    }

    void ShadowBolt()
    {
        audio.Play();
        canFire = Time.time + fireRate;
        Instantiate(shadowBolt, startPos.transform.position, startPos.transform.rotation);
    }
}
