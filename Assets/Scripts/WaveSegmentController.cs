﻿using UnityEngine;
using System.Collections;
using System;

public class WaveSegmentController : MonoBehaviour {

	public float wavelength, velocity, refractiveIndex, deltaV;

    private float prevMaterialPhase, prevRefractiveIndex, prevPhase, holdTime;

    private Rigidbody rb;
	private Renderer rend;
    
    void Start () {
        rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer> ();
		rend.material.SetFloat ("_Velocity", velocity);
		rend.material.SetFloat ("_TransitionPhase", 0.0f);
		prevMaterialPhase = 0.0f;
		prevPhase = 0.0f;
    }

	void Update() {
		if (transform.position.x > 10 && transform.position.x < 200) {
			if (refractiveIndex != 0.5f) {
				refractiveIndex = 0.5f;
			}
		} else {
			if (refractiveIndex != 1.0f) {
				refractiveIndex = 1.0f;
			}
		}

		rend.material.SetFloat ("_RefractiveIndex", refractiveIndex);
	}
    
    void FixedUpdate () {
        if (Input.GetAxis ("Vertical") > 0) {
            velocity += deltaV;
			rend.material.SetFloat ("_Velocity", velocity);
        } else if (Input.GetAxis ("Vertical") < 0) {
			velocity -= deltaV;
			rend.material.SetFloat ("_Velocity", velocity);
        }

		if (Input.GetAxis ("Horizontal") > 0) {
            if (wavelength < 0.513) {
                wavelength += 0.0001f;
            }
			rend.material.SetFloat ("_Wavelength", wavelength);
		} else if (Input.GetAxis ("Horizontal") < 0) {
            if (wavelength > 0.486) {
			    wavelength -= 0.0001f;
            }
			rend.material.SetFloat ("_Wavelength", wavelength);
		}
		
		Vector3 newPos = new Vector3 (transform.position.x + velocity * refractiveIndex, transform.position.y, transform.position.z);
		rb.MovePosition (newPos);
    }
}
