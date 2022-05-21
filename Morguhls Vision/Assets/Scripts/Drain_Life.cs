using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using TMPro;

public class Drain_Life : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject Castbar;
    [SerializeField] LineController line;
    [SerializeField] Transform[] points;
    [SerializeField] Image fill;
    [SerializeField] private bool isDraining;

    public bool IsDraining
    {
        get { return isDraining; }
        set { isDraining = value; }
    }
    [SerializeField] Text castTimeText;
    float castTime = 0f;
    int targetHealth = 15;
    bool isSelected = false;
    bool isActive = false;
    bool isOnCD = false;
    [SerializeField] float cooldownTimer;
    [SerializeField] Texture2D normalPointer;
    [SerializeField] Texture2D actionPointer;
    Vector2 hotspot = Vector2.zero;
    Ray ray;
    RaycastHit hit;
    [SerializeField] Image cooldownImage;
    [SerializeField] GameObject description;
    bool isOver = false;
    Enemy_Behavior enemy;
    PlayerHealth playerHP;
    [SerializeField] GameObject ButtonPressed;
    Transform playerTransform;
    [SerializeField] TextMeshProUGUI rangeTxt;
    bool isTextShown = false;
    float showTextTimer = 2;
    Time_Stop ts;
    //[SerializeField] GameObject drain;


    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<Enemy_Behavior>().GetComponent<Enemy_Behavior>();
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
        playerHP = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
        playerTransform = FindObjectOfType<Movement>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !ts.IsStopped)
        {
            DrainLife();
        }
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        if (Vector3.Distance(playerTransform.position, hit.transform.position) > 10)
                        {
                            rangeTxt.gameObject.SetActive(true);
                            isTextShown = true;
                            isSelected = false;
                            Cursor.SetCursor(normalPointer, hotspot, CursorMode.Auto);
                            ButtonPressed.SetActive(false);
                        }
                        else
                        {
                            points[0] = hit.transform;
                            playerHP.GetHealth(30);
                            Castbar.SetActive(true);
                            isDraining = true;
                            line.SetUpLine(points);
                            Cursor.SetCursor(normalPointer, hotspot, CursorMode.Auto);
                            isSelected = false;
                            //Instantiate(drain, points[0].position, points[0].rotation);
                        }
                    }
                    else
                    {
                        isSelected = false;
                        Cursor.SetCursor(normalPointer, hotspot, CursorMode.Auto);
                        ButtonPressed.SetActive(false);
                    }
                }
            }
        }
        if (isDraining)
        {
            playerTransform.LookAt(points[0]);
            //playerHP.CurrentHealth += targetHealth;
            fill.fillAmount -= Time.deltaTime * 0.5f;
            castTime += Time.deltaTime;
            castTimeText.text = castTime.ToString("0.0 / 2");
            points[0].GetComponent<Enemy_Behavior>().GetDamage(targetHealth * Time.deltaTime);
            if (fill.fillAmount <= 0 || Input.GetKeyDown(KeyCode.Escape) || points[0].GetComponent<Enemy_Behavior>().CurrentHealth <= 0)
            {
                points[0] = null;
            }
            if(points[0] == null)
            {
                line.CancelLine();
                Castbar.SetActive(false);
                isDraining = false;
                fill.fillAmount = 1;
                isActive = true;
                castTime = 0;
            }
        }
        if (isActive)
        {
            ButtonPressed.SetActive(false);
            cooldownImage.gameObject.SetActive(true);
            cooldownImage.fillAmount -= cooldownTimer * Time.deltaTime;
            isOnCD = true;
            isSelected = false;
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
                rangeTxt.gameObject.SetActive(false);
                showTextTimer = 2;
                isTextShown = false;
            }
        }
    }

    public void DrainLife()
    {
        if (!Gamemanager.GM.isStart) return;
        if (!isOnCD && !ts.IsStopped)
        {
            ButtonPressed.SetActive(true);
            isSelected = true;
            Cursor.SetCursor(actionPointer, hotspot, CursorMode.Auto);
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
