using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public Transform target;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // LateUpdate wegen Kamera
    void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = targetCamPos;
    }


    void OnTriggerExit(Collider other)
    {
           if(other.gameObject.tag == "Player")     
            Destroy(other.gameObject);
        
    }	
}
