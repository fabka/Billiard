using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

	ArrayList collisionPoints;

	// Use this for initialization
	void Start () {
		collisionPoints = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter ( Collision c ){
		collisionPoints.Add( c.contacts [0].point );
	}

	void OnMouseDown(){
		Debug.ClearDeveloperConsole ();
		Debug.Log ("There we go");
		for (int i=0; i<collisionPoints.Count; i++) {
			Debug.Log( collisionPoints[i].ToString() );
		}
	}
}
