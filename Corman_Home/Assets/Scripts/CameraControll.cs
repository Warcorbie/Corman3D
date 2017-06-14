﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

    public Transform target;
    public float smoothing;

    Vector3 offset;

	void Start () {
        offset = transform.position - target.transform.position;
	}
	
	// LateUpdate wegen Kamera
	void LateUpdate () {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
