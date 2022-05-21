using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    int startHealth = 50;
    int maxHealth = 100;
    [SerializeField] AudioSource audio;
    AudioSource[] allAudio;
    public float audioTImer = 1;
    public bool isDead = false;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    Animator anim = null;
    [SerializeField] GameObject deathPanel;

    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            healthGlobe.SetInt("_Health", currentHealth);
            currentHealth = value;
        }
    }

    [SerializeField] Material healthGlobe;
    int resistence = 4;
    //[SerializeField] TextMeshProUGUI text;
    //[SerializeField] GameObject ShowHealth;
    bool isOver = false;
    private void Awake()
    {
        healthGlobe.GetFloat("_Health");
        //healthGlobe.SetFloat("_Health", startHealth);
        //CurrentHealth = startHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        healthGlobe.SetFloat("_Health", startHealth);
        currentHealth = startHealth;
        Gamemanager.GM.enemyAttackTransform.Add(this.transform);
        allAudio = FindObjectsOfType<AudioSource>();
        //text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = currentHealth.ToString();
        //if (isOver)
        //{
        //    ShowHealth.SetActive(true);
        //}
        //else
        //{
        //    ShowHealth.SetActive(false);
        //}
        //if (isDead)
        //{
        //    audioTImer -= Time.deltaTime;
        //    if (audioTImer <= 0)
        //    {
        //        audio.Play();
        //    }
        //}
        if (CurrentHealth >= MaxHealth) CurrentHealth = MaxHealth;
        if(CurrentHealth <= 0)
        {
            //if (!isDead)
            //{
            //    foreach(AudioSource sources in allAudio)
            //    {
            //        sources.Stop();
            //    }
            //    isDead = true;
            //}
            //else
            //{
            //    audio.Play();

            //}
            anim.Play("Dead");
            anim.SetBool("IsStop", false);
            anim.SetBool("Raise", false);
            deathPanel.SetActive(true);
            Gamemanager.GM.enemyAttackTransform.Remove(this.transform);
        }
    }

    public void GetDamage(int damage)
    {
        CurrentHealth -= Mathf.Clamp(damage, 0, int.MaxValue);
        healthGlobe.SetInt("_Health", currentHealth);
    }
    public void GetHealth(int health)
    {
        CurrentHealth += Mathf.Clamp(health, 0, int.MaxValue);
        healthGlobe.SetInt("_Health", currentHealth);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Enter");
        isOver = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("Exit");
        isOver = false;
    }
}
