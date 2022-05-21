using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthGlobe : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isOver = false;
    PlayerHealth ph;
    [SerializeField] GameObject ShowHealth;
    [SerializeField] TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        ph = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = string.Format("Leben: {0} / 100", ph.CurrentHealth);
        //if (isOver)
        //{
        //    ShowHealth.SetActive(true);
        //}
        //else
        //{
        //    ShowHealth.SetActive(false);
        //}
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
