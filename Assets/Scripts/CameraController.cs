using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject followSegment;

	public float cameraHeight;

    private Vector3 offset;

    void Start ()
    {
        offset = transform.position - followSegment.transform.position;
    }

    void LateUpdate ()
    {
        Vector3 oscillatingLocation = followSegment.transform.position + offset;
        transform.position = new Vector3(oscillatingLocation.x, cameraHeight, oscillatingLocation.z);
    }
}
