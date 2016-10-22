using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = FindObjectOfType<PlayerController>().transform.localPosition;
        transform.position = new Vector3(pos.x, pos.y, -10);
	}

}
