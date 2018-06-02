﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public float speed = 1;
	float value;
	Rigidbody2D myBody = null;
	
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	
	// Use this for initialization
	void Start  () {
		value = Input.GetAxis ("Horizontal");
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelController.current.setStartPosition (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		Animator animator = GetComponent<Animator> ();
		animator.SetBool ("run", Mathf.Abs(value) > 0);
		animator.SetBool ("jump", !this.isGrounded);
	
	}
	
	void FixedUpdate () {
		value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
		Vector3 fr = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast(fr, to, layer_id);
		isGrounded = hit;
		
		if (Input.GetButtonDown("Jump") && isGrounded)
			this.JumpActive = true;
		
		if(this.JumpActive) {
			if(Input.GetButton("Jump")) {
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}
		
	}
}
