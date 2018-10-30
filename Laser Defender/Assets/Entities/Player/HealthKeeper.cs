using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour {
	
	public static float health = 600f;
	private Text myText;
	void Start(){
		myText = GetComponent<Text>();
	}
	
	
	public void Health(float damage){
		Debug.Log ("Took 100 damage!");
		health -= damage;
		myText.text = health.ToString ();
	}
	
	public static void Reset(){
		health = 600f;
	}
}
