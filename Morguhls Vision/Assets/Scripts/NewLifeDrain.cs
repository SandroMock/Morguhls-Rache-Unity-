using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class NewLifeDrain : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform enemy;
    [SerializeField] Transform player;
    Time_Stop ts;
    [SerializeField] TextMeshProUGUI rangeTxt;
    bool isTextShown = false;
    float showTextTimer = 2;
    bool isOver = false;
    [SerializeField] GameObject description;
    bool isActive = false;
    [SerializeField] GameObject ButtonPressed;
    [SerializeField] Image cooldownImage;
    [SerializeField] float cooldownTimer;
    bool isOnCD = false;
    PlayerHealth playerHP;
    [SerializeField] GameObject effect;
    [SerializeField] AudioSource audio;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject mesh;
    float meshTimer = 1.4f;


    // Start is called before the first frame update
    void Start()
    {
        enemy = Gamemanager.GM.GetNearestEnemy(player.transform.position);
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
        playerHP = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        enemy = Gamemanager.GM.GetNearestEnemy(player.transform.position);
        if (Input.GetKeyDown(KeyCode.Alpha3) && !ts.IsStopped && !isOnCD)
        {
            Drainlife();
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
        if (isOver)
        {
            description.SetActive(true);
        }
        else
        {
            description.SetActive(false);
        }
        if (isActive)
        {
            ButtonPressed.SetActive(false);
            cooldownImage.gameObject.SetActive(true);
            cooldownImage.fillAmount -= cooldownTimer * Time.deltaTime;
            isOnCD = true;
            if (cooldownImage.fillAmount <= 0)
            {
                isActive = false;
                isOnCD = false;
                cooldownImage.fillAmount = 360;
                cooldownImage.gameObject.SetActive(false);
            }
        }
        if (mesh.activeSelf)
        {
            meshTimer -= Time.deltaTime;
            if (meshTimer <= 0)
            {
                mesh.SetActive(false);
                meshTimer = 1.4f;
            }
        }
    }

    public void Drainlife()
    {
        if (!Gamemanager.GM.isStart) return;
        if(enemy == null)
        {
            enemy = Gamemanager.GM.GetNearestEnemy(player.transform.position);
            if(enemy == null)
            {
                rangeTxt.gameObject.SetActive(true);
                isTextShown = true;
            }
        }
        else
        {
            if(enemy != null && Vector3.Distance(player.transform.position, enemy.position) > 10)
            {
                rangeTxt.gameObject.SetActive(true);
                isTextShown = true;
            }
            if(enemy != null && Vector3.Distance(player.transform.position, enemy.position) <= 10)
            {
                mesh.SetActive(true);
                Instantiate(effect, enemy.position + offset, enemy.rotation);
                audio.Play();
                playerHP.GetHealth(30);
                enemy.GetComponent<Enemy_Behavior>().GetDamage(30);
                isActive = true;
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
