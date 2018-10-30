using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	void OnDrawGizmos(){//gizmos are made visible to help you design games better
		Gizmos.DrawWireSphere (transform.position, 1f);
	}
}
