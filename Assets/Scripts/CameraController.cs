using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject followSegment;

	private Vector3 offset;

	void Start ()
	{
		offset = transform.position - followSegment.transform.position;
	}

	void LateUpdate ()
	{
            Vector3 oscillatingLocation = followSegment.transform.position + offset;
            transform.position = new Vector3(oscillatingLocation.x, 10.0f, oscillatingLocation.z);
	}
}
