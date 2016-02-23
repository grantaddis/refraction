using UnityEngine;
using System.Collections;
using System;

public class WaveSegmentController : MonoBehaviour {

	public float wavelength, velocity;

    private Rigidbody rb;
	private Renderer rend;
    
    void Start () {
        rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer> ();
		rend.material.SetFloat ("_Velocity", velocity);
    }
    
    void FixedUpdate () {
        if (Input.GetAxis ("Vertical") > 0) {
            velocity += 1.0f;
			rend.material.SetFloat ("_Velocity", velocity);
        } else if (Input.GetAxis ("Vertical") < 0) {
			rend.material.SetFloat ("_Velocity", velocity);
            velocity -= 1.0f;
        }

		if (Input.GetAxis ("Horizontal") > 0) {
            if (wavelength < 0.513) {
                wavelength += 0.0001f;
            }
			rend.material.SetFloat ("_Wavelength", wavelength);
		} else if (Input.GetAxis ("Horizontal") < 0) {
            if (wavelength > 0.485) {
			    wavelength -= 0.0001f;
            }
			rend.material.SetFloat ("_Wavelength", wavelength);
		}
        
		Vector3 newPos = new Vector3 (transform.position.x + velocity * Time.deltaTime, transform.position.y, transform.position.z);
		rb.MovePosition(newPos);
    }
}