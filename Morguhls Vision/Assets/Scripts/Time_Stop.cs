using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;

public class Time_Stop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject CastBar;
    [SerializeField] Image fill;
    float castTime = 5f;
    [SerializeField] Text castTimeText;
    bool isOver = false;
    [SerializeField] GameObject description;
    [SerializeField] GameObject ButtonPressed;
    bool isActive = false;
    [SerializeField] Image cooldownImage;
    bool isOnCD = false;
    [SerializeField] float cooldownTimer;
    PlayerHealth playerHP;
    [SerializeField] TextMeshProUGUI sceletonCountText;
    float showTextTimer = 2;
    bool isTextShown = false;
    [SerializeField] AudioSource audio;
    [SerializeField] TextMeshProUGUI lowHealthTxt;


    public bool IsStopped { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerHP = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && playerHP.CurrentHealth > 25)
        {
            //IsStopped = true;
            TimeStop();
        }
        if (IsStopped)
        {
            CastBar.SetActive(true);
            fill.fillAmount -= Time.deltaTime * .2f;
            castTime -= Time.deltaTime;
            castTimeText.text = castTime.ToString("0.0 / 5");
            if (fill.fillAmount <= 0 || Input.GetKeyDown(KeyCode.Escape))
            {
                IsStopped = false;
                CastBar.SetActive(false);
                fill.fillAmount = 1;
                castTime = 5;
                ButtonPressed.SetActive(false);
                isActive = true;
            }
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

        if (isOver)
        {
            description.SetActive(true);
        }
        else
        {
            description.SetActive(false);
        }
        if (isTextShown)
        {
            showTextTimer -= Time.deltaTime;
            if (showTextTimer <= 0)
            {
                lowHealthTxt.gameObject.SetActive(false);
                sceletonCountText.gameObject.SetActive(false);
                showTextTimer = 2;
                isTextShown = false;
            }
        }
    }

    public void TimeStop()
    {
        if (!Gamemanager.GM.isStart) return;
        if(playerHP.CurrentHealth <= 25)
        {
            lowHealthTxt.gameObject.SetActive(true);
            isTextShown = true;
            return;
        }
        if (!isOnCD)
        {
            if(Gamemanager.GM.sceletonTransform.Count > 0)
            {
                audio.Play();
                ButtonPressed.SetActive(true);
                playerHP.GetDamage(25);
                IsStopped = true;
            }
            else
            {
                sceletonCountText.gameObject.SetActive(true);
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
