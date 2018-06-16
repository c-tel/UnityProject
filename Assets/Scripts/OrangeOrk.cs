using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeOrk : MonoBehaviour {
	private float direction = 1f;
	private float x1, x2;
	private Rigidbody2D myBody = null;
	private SpriteRenderer sr = null;
	private Animator animator;
	private float attackTime;

	public float coolDownTime = 3f;
	public float area = 3f;
	public float speed = 10f;
	public Carrot carrot;



	void Start () 
	{
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
		if (attackTime > 0){
			attackTime -= Time.fixedDeltaTime;
		} 
		if (RabbitHere ()) {
			animator.SetBool("run", false);
			float buff = direction;
			direction =	(HeroRabbit.lastRabbit.transform.position.x - this.transform.position.x) > 0 ? 1 : -1;
			if (buff != direction)
				sr.flipX = !sr.flipX;
			if(attackTime<=0)
				AttackWithCarrot ();	
			
			
		} else {
			animator.SetBool("run", true);
			if (isArrived ()) {
				direction *= -1;
				sr.flipX = !sr.flipX;
			}
			Vector2 vel = myBody.velocity;
			vel.x = direction * (speed * 0.1f);
			myBody.velocity = vel;
		}
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
			animator.SetTrigger("death");
		} 
	}
	

	bool RabbitHere()
	{
		float rabbit_x = HeroRabbit.lastRabbit.transform.position.x;
		float rabbit_y = HeroRabbit.lastRabbit.transform.position.y;
		float my_x = transform.position.x;
		float my_y = transform.position.y;

		return Math.Abs(rabbit_x-my_x)<5f && Math.Abs(rabbit_y-my_y)<0.4f; 
	}

	void Die()
	{
		Destroy (this.gameObject);
	}

	void AttackWithCarrot()
	{
		attackTime = coolDownTime;
		Carrot carr = Instantiate (carrot.gameObject).GetComponent<Carrot> ();
		carr.transform.position = transform.position + new Vector3 (0, 0.5f, 0);
		animator.SetTrigger("beat");
		carr.speed = 0.1f;
		carr.lifeDuration = 5f;
		carr.direction = direction;

	}
}
