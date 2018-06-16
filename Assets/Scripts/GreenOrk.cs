using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrk : MonoBehaviour {
	private float direction = 1f;
	private float x1, x2;
	private Rigidbody2D myBody = null;
	private SpriteRenderer sr = null;
	private Animator animator;

	public float area = 3f;
	public float speed = 10f;
	private float low_speed, high_speed, curr_speed;
	// Use this for initialization
	void Start () 
	{
		low_speed = speed;
		high_speed = speed * 2f;
		animator = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer>();
		sr.flipX = !sr.flipX;
		x1 = this.transform.position.x;
		x2 = x1+area;
		myBody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		animator.SetBool ("rabbit_here", RabbitHere ());
		if (RabbitHere ()) {
			curr_speed = high_speed;
			float buff = direction;
			direction =	(HeroRabbit.lastRabbit.transform.position.x - this.transform.position.x) > 0 ? 1 : -1;
			if (buff != direction)
				sr.flipX = !sr.flipX;
		} else {
			curr_speed = low_speed;
			if (isArrived ()) {
				direction *= -1;
				sr.flipX = !sr.flipX;
			}
		}
		Vector2 vel = myBody.velocity;
		vel.x = direction*(curr_speed*0.01f);
		myBody.velocity = vel;
	}

	bool isArrived() {
		float target = direction==1 ? x2 : x1;
		float pos = this.transform.position.x;
		return Math.Abs(pos - target) < 0.02f;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name != "HeroRabbit")
			return;
		
		Collider2D collider = collision.collider;
		Vector3 contactPoint = collision.contacts[0].point;
		Vector3 center = collider.bounds.center;
		if (Math.Abs (contactPoint.y - center.y) > 0.4) {
			direction = 0;
			animator.SetTrigger ("death");
		} else {
			animator.SetTrigger ("kick");
			HeroRabbit.lastRabbit.AnimatedDeath ();
		}
	}

	bool RabbitHere()
	{
		float rabbit_x = HeroRabbit.lastRabbit.transform.position.x;
		float rabbit_y = HeroRabbit.lastRabbit.transform.position.y;

		return rabbit_x>=x1 && rabbit_x<=x2 && Math.Abs(rabbit_y-this.transform.position.y)<0.4; 
	}

	void Die()
	{
		Destroy (this.gameObject);
	}

}
