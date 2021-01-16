using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
    private float dampTime = 0.35f;
    private Vector3 velocity = Vector3.zero;
    private Transform target;

	void Start() {
		target = GameObject.FindObjectOfType<Player>().transform;
	}
 
    void Update () {
        if (target) {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.25f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }
     
     }
}
