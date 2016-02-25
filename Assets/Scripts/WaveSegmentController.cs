using UnityEngine;
using System.Collections;
using System;

public class WaveSegmentController : MonoBehaviour {

	public float wavelength, velocity, refractiveIndex;

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
		holdTime = 0.0f;
    }
    
    void FixedUpdate () {
		if (transform.position.x > 10 && transform.position.x < 200) {
			if (refractiveIndex != 0.5f) {
				refractiveIndex = 0.5f;
				rend.material.SetFloat ("_TransitionPhase", 10.0f);
				holdTime = (2.0f / (refractiveIndex * velocity));
				prevMaterialPhase = prevPhase;
			}
		} else {
			if (refractiveIndex != 1.0f) {
				refractiveIndex = 1.0f;
				rend.material.SetFloat ("_TransitionPhase", 0.0f);
				holdTime = (2.0f / (refractiveIndex * velocity));
				prevMaterialPhase = prevPhase;
			}
		}

		rend.material.SetFloat ("_RefractiveIndex", refractiveIndex);

        if (Input.GetAxis ("Vertical") > 0) {
            velocity += 1.0f;
			rend.material.SetFloat ("_Velocity", velocity);
        } else if (Input.GetAxis ("Vertical") < 0) {
			velocity -= 1.0f;
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
        
		if (holdTime == 0.0f) {
			Vector3 newPos = new Vector3 (transform.position.x + velocity * refractiveIndex * Time.deltaTime, transform.position.y, transform.position.z);
			rb.MovePosition (newPos);
		} else if (Time.deltaTime > holdTime) {
			holdTime = 0.0f;
		} else {
			holdTime -= Time.deltaTime;
		}

		prevPhase = prevMaterialPhase + ((2.0f * Mathf.PI / wavelength) * (Time.time * velocity * refractiveIndex + transform.position.x));
    }
}