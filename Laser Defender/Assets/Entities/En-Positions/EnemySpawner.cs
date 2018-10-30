using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	private float xMax;
	private float xMin;
	private bool movingRight = false;
	//******************************************************************************
	// Use this for initialization
	void Start () {
		SpawnEnemies();
		
	}
	//***********************************************

	// Update is called once per frame
	void Update () {
		if(movingRight){
			shiftRight ();
		}else{
			shiftLeft ();
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(leftEdgeOfFormation < xMin){
			movingRight = true ;
		} else if (rightEdgeOfFormation > xMax  ){
		 movingRight = false;
		}
		if(AllMembersDead()){
			Debug.Log ("New wave inbound");
			SpawnUntilFull();
			
		}
	
	}
	//****************************************************************************************
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0f) );
	}
	//**************************
	//**********************************************************************************************
	void shiftRight(){transform.position += Vector3.right * speed * Time.deltaTime;}
	//****************
	void shiftLeft(){transform.position += Vector3.left * speed * Time.deltaTime;}
	//******************************
	bool AllMembersDead(){
		//we need to loop over every position in the formation
		//and check if that position has a child (a live enemy)
		foreach(Transform childPositionGameObject in transform){ //because this script is attached to the formation GameObject
			//transforms keep track of the children and parent in hierarchy 
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
	//**************************************************************************************
	void SpawnEnemies(){
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3(-0.1f, 0f, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3(1.1f, 0f, distanceToCamera));
		xMax = rightEdge.x;
		xMin = leftEdge.x;
		foreach( Transform child in transform){
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
	}
}
	//*******************************************************************************************
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){ //because this script is attached to the formation GameObject
			//transforms keep track of the children and parent in hierarchy 
			if(childPositionGameObject.childCount == 0){//if the position is empty, it is returned and a new enemy is spawned there
				return childPositionGameObject;
			}
		}
		return null;//if no position is free, nothing is returned
	}// this returns the next free position in the method
	//*****************************************************************************************
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if(freePosition){
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition ()){
		Invoke ("SpawnUntilFull", spawnDelay);//we used the function to call itself. This is Recursion
		}
	}//this method works along with NextFreePosition
	// if NextFreePosition() returns a transform (with no enemy child object at that position)
	//then SpawnUntilFull() instantiates an enemy child there with some delay.
	//*****************************************************************************************
}
