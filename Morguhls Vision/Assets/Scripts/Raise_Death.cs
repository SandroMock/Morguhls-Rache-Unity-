using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;



public class Raise_Death : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image cooldownImage;
    bool isOnCD = false;
    public static bool isActive = false;
    [SerializeField] float cooldownTimer;
    [SerializeField] GameObject sceleton;
    [SerializeField] GameObject player;
    GameObject[] corpse;
    [SerializeField] TextMeshProUGUI noCorpseTxt;
    [SerializeField] TextMeshProUGUI lowHealthTxt;
    float showTextTimer = 2;
    bool isTextShown = false;
    [SerializeField] GameObject description;
    bool isOver = false;
    PlayerHealth playerHP;
    Time_Stop ts;
    [SerializeField] AudioSource audio;
    [SerializeField] TextMeshProUGUI sceletonAmmount;


    // Start is called before the first frame update
    void Start()
    {
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
        playerHP = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        corpse = GameObject.FindGameObjectsWithTag("Corpse");
        if(Input.GetKeyDown(KeyCode.Alpha1) && !isOnCD && !ts.IsStopped && playerHP.CurrentHealth > 20)
        {
            RaiseDeath();
        }
        if(isActive)
        {
            cooldownImage.fillAmount -= cooldownTimer * Time.deltaTime;
            isOnCD = true;
            if(cooldownImage.fillAmount <= 0)
            {
                isActive = false;
                isOnCD = false;
                cooldownImage.fillAmount = 360;
                cooldownImage.gameObject.SetActive(false);
            }
        }
        if (isTextShown)
        {
            showTextTimer -= Time.deltaTime;
            if(showTextTimer <= 0)
            {
                noCorpseTxt.gameObject.SetActive(false);
                lowHealthTxt.gameObject.SetActive(false);
                showTextTimer = 2;
                isTextShown = false;
            }
        }
        if (isOver)
        {
            description.SetActive(true);
        }
        else
        {
            description.SetActive(false);
        }

        sceletonAmmount.text = Gamemanager.GM.sceletonTransform.Count.ToString();
    }
    public void RaiseDeath()
    {
        if (!Gamemanager.GM.isStart) return;
        if(playerHP.CurrentHealth <= 20)
        {
            lowHealthTxt.gameObject.SetActive(true);
            isTextShown = true;
            return;
        }
        if (corpse.Length < 1)
        {
            noCorpseTxt.gameObject.SetActive(true);
            isTextShown = true;
            return;
        }
        if (!isOnCD)
        {
            int spawn = Random.Range(0, corpse.Length);
            GameObject spawnPoint = corpse[spawn];
            if (Vector3.Distance(player.transform.position, spawnPoint.transform.position) <= 10 && corpse.Length > 0)
            {
                audio.Play();
                cooldownImage.gameObject.SetActive(true);
                GameObject newSceleton = Instantiate(sceleton, spawnPoint.transform.position, Quaternion.identity);
                Destroy(spawnPoint);
                isActive = true;
                playerHP.GetDamage(20);
            }
            else if(
                (Vector3.Distance(player.transform.position, spawnPoint.transform.position) <= 10 &&
                corpse.Length <= 0) || Vector3.Distance(player.transform.position, spawnPoint.transform.position) > 10 &&
                (corpse.Length <= 0 || corpse.Length > 0))
            {
                noCorpseTxt.gameObject.SetActive(true);
                isTextShown = true;
            }
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isOver = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isOver = false;
    }
}
