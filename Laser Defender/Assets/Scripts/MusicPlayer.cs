using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip music;
	void Awake(){
		//Debug.Log ("Music player Awake " + GetInstanceID ());//Everytime we get to the start scene, a new Music Player is momentarily created and has a different Instance ID than the one that is initially created when we first start the game. Duto the self-destruct statement, it gets destroyed.
		if(instance != null){
			Destroy (gameObject);
			print ("Duplicate MusicPlayer self-destruct initiated!" + GetInstanceID ());
		}else{
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	//Debug.Log ("Music player start " + GetInstanceID () );

		AudioSource.PlayClipAtPoint (music,transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
