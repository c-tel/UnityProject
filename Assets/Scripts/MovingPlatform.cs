using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public Vector3 moveBy;
	public float stop_time = 2f;
	public float speed = 0.001f;
	private Vector3 pointA;
	private Vector3 pointB;
	private float wait_time;
	private bool going_to_a = false;

	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + moveBy;
		wait_time = this.stop_time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (this.wait_time > 0) {
			this.wait_time -= Time.deltaTime;
			return;
		}
		this.transform.position += (moveBy * speed) * (going_to_a ? -1 : 1);
		if (isArrived ()) {
			this.going_to_a = !this.going_to_a;
			this.wait_time = stop_time;
		}
	}

	bool isArrived() {
		Vector3 target = going_to_a ? pointA : pointB;
		Vector3 pos = this.transform.position;
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}
}
