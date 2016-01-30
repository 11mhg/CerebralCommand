using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	private int movspeed;
	private float horizontalMove;
	private float verticalMove;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		horizontalMove = 0;
		verticalMove = 0;
		rb2d = GetComponent<Rigidbody2D>();
		movspeed = 700;
	}

	void Update () {
		rotateCharacter();
		moveCharacter();
	}

	void moveCharacter(){
		horizontalMove = Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
		verticalMove = Input.GetAxisRaw ("Vertical") * Time.deltaTime;
		Vector2 movement = new Vector2(horizontalMove*movspeed,verticalMove*movspeed);
		rb2d.velocity = movement;
	}

	void rotateCharacter(){
		Vector2 playerPos = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mousePos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = angleBetweenTwoPoints (playerPos,mousePos);
		transform.rotation = Quaternion.Euler (new Vector3( 0f, 0f,angle+90));
	}

	float angleBetweenTwoPoints(Vector3 a, Vector3 b){
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}
