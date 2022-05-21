using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth_Follow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float dist = 5;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 5;
    [SerializeField] float scrollSens = 1;
    [SerializeField] float maxDist = 7;
    [SerializeField] float minDist = 1;
    

    private void LateUpdate()
    {
        if (!target) return;

        float num = Input.GetAxis("Mouse ScrollWheel");
        dist -= num * scrollSens;
        dist = Mathf.Clamp(dist, minDist, maxDist);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * dist;

        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
    }
}
