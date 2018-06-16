using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public static HeroRabbit lastRabbit;
	public float speed = 1;
	float value;
	Rigidbody2D myBody = null;
	
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	private bool enlarged;

	Transform heroParent = null;

	void Awake(){
		lastRabbit = this;
		this.gameObject.name = "HeroRabbit";
	}

	// Use this for initialization
	void Start  () {
		this.heroParent = this.transform.parent;
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

		if(hit) {
			//Перевіряємо чи ми опинились на платформі
			if(hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		} else {
			//Ми в повітрі відліпаємо під платформи
			SetNewParent(this.transform, this.heroParent);
		}

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

	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}

	public void AnimatedDeath(){
		myBody.velocity = Vector2.zero;
		Animator animator = GetComponent<Animator> ();
		animator.SetTrigger ("deathly_hit");
	}

	public void Death(){
		LevelController.current.onRabbitDeath (this);
		GetComponent<Animator> ().SetTrigger ("reset");
	}

	public bool Enlarged(){
		return enlarged;
	}

	public void Enlarge(){
		if (enlarged)
			return;
		this.enlarged = true;
		this.transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
	}

	public void Reduce(){
		this.enlarged = false;
		this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
