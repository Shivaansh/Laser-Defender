using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {
	
	//************** new stuff for enemy fire*********
	public GameObject enemyLaser;
	public float EnemylaserSpeed;
	public float ShotPerSec;
	public int scoreValue = 100;
	public AudioClip EnemyFireSound;
	public AudioClip EnemyDeathSound;
	private ScoreKeeper scoreKeeper;
	
	//***************************************************

	public float health = 150f;
	
	void Start(){
		//scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();// this didn't work because our scoring object is not called Score, it is called ScoreText
		scoreKeeper = GameObject.Find("ScoreText").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		//Instantiate (enemyLaser);
		//**************** new stuff******************
		float probability = Time.deltaTime*ShotPerSec; // Time.deltaTime is the time elapsed,ShotPerSec is frequency
		if(Random.value < probability ){
		Fire ();
		}
		//******************************************
	}

	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		//above, for collider.gameObject, gameObject is object we collide with, and collider. is coz we are getting a collider component
		// missile is a child of the gameObject component 
		if(missile){
			health -= missile.GetDamage ();
			missile.Hit ();
			if(health <= 0) {
				
				Destroy (gameObject);
				scoreKeeper.Score(scoreValue);
				AudioSource.PlayClipAtPoint (EnemyDeathSound,transform.position);
			
			}
			Debug.Log ("Hit by projectile");
		}
		//******************** works perfectly till here**********************************
		}
		
		void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject missile = Instantiate (enemyLaser , startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -EnemylaserSpeed);
		AudioSource.PlayClipAtPoint (EnemyFireSound,transform.position);
		}
		//*************************************

	}

