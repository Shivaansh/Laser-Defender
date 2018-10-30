using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
public float speed = 100.0f;
public GameObject laser;
public float laserSpeed = 100f;
public float playerhealth = 600f;
public float damage = 100f;
public AudioClip PlayerFireSound;
public AudioClip PlayerDeathSound;
private HealthKeeper healthKeeper; //this is how you create an instance[healthKeeper] of a class [HealthKeeper]

	float xmin; //=-6f
	float xmax; //=6f
	float ymin = -4f;
	float ymax = 4f;
	public float padding = 1f;
	public float fireRate = 0.2f;
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3(0f ,0f ,distance )); //we use these to limit movement to camera frame
		Vector2 rightmost = Camera.main.ViewportToWorldPoint (new Vector3(1f ,0f ,distance ));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		
		healthKeeper = GameObject.Find("Health").GetComponent<HealthKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)|| Input.GetKey (KeyCode.LeftArrow) ){moveLeft ();}
		
		else if(Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)){moveRight ();}
		
		if(Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)){moveDown ();}
		
		else if(Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)){moveUp ();}
		
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);
		//restrict player to the game space
		transform.position = new Vector3(newX, newY, transform.position.z);
		
		
		
		
		if(Input.GetKeyDown (KeyCode.Space)){
			InvokeRepeating ("Fire", 0.0001f, fireRate); //used to add some delay between shots
		}
		if(Input.GetKeyUp (KeyCode.Space)){
			CancelInvoke ("Fire");
		}
		//here, when we press the space bar, it starts the Fire method in a loop, which ends when space is released.
		// therefore, we have essentially executed GetKey using GetKeyDown.
		
	}
	
	

	
	void moveLeft(){
		transform.position += new Vector3(-speed*Time.deltaTime, 0f, 0f);
		Debug.Log ("Strafing left");
	}
	
	void moveRight(){
		transform.position += new Vector3(speed*Time.deltaTime, 0f, 0f);
		Debug.Log ("Strafing right");
	}
	
	void moveUp(){
		transform.position += new Vector3(0f, speed*Time.deltaTime, 0f);
		Debug.Log ("Fusion thrusters active, approaching terminal velocity");
		}
	
	void moveDown(){
		transform.position += new Vector3(0f, -speed*Time.deltaTime, 0f);
		Debug.Log ("Reverse thrusters active, returning to nominal velocity");
		}
	void Fire(){
		Vector3 offset = new Vector3(0f, 0.6f, 0f);
		GameObject laserBeam = Instantiate (laser, transform.position + offset , Quaternion.identity) as GameObject;
		laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, laserSpeed*10, 0f);
		AudioSource.PlayClipAtPoint (PlayerFireSound,transform.position);
		
	}
	//****************** new stuff for being hit by enemy projectile**********
void OnTriggerEnter2D(Collider2D collider){
	Projectile missile = collider.gameObject.GetComponent<Projectile>();
	//above, for collider.gameObject, gameObject is object we collide with, and collider. is coz we are getting a collider component
	// missile is a child of the gameObject component 
	if(missile){
		playerhealth -= missile.GetDamage ();
		healthKeeper.Health (damage);
		missile.Hit ();
		if(playerhealth <= 0) {
			
			Destroy (gameObject);
			AudioSource.PlayClipAtPoint (PlayerDeathSound,transform.position);
			Application.LoadLevel ("GameOver");
			
		}
		Debug.Log ("Structural integrity reduced!");
	}
}
}
	//**************************************************************************
	

	


