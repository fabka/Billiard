using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class InstantiateBalls : MonoBehaviour {

	public GameObject ball;
	private GameObject yellow;
	private GameObject white;
	private GameObject red;
	private GameObject empty;

	private LineRenderer lineRenderer;

	private StreamReader sr;
	private string str;

	private Vector3 lastPoint;
	private Vector3 actualPoint;

	ArrayList collisionPoints;
	
	// Use this for initialization

	void Start () {
		initializeBalls ();
		//getPoints ();
	}

	void initializeBalls(){
		Vector3[] points = getPoints ();
		InstantiateBall (ref white, "white", points[0]/*new Vector3 (0.5f, 0.075f, 0.5f)*/, Color.white);
		InstantiateBall (ref yellow, "yellow", points[1]/*new Vector3 (1.964f, 0.075f, 0.596f)*/, Color.yellow);
		InstantiateBall (ref red, "red", points[2]/*new Vector3 (1f, 0.075f, 1f)*/, Color.red);

		lastPoint = white.transform.position;
	}

	void Update(){
		Rigidbody rb = white.GetComponent<Rigidbody> ();
		//Debug.Log ("velocity = " + rb.velocity.magnitude );
		if (Input.GetMouseButtonDown (0) && rb.velocity.magnitude < 0.01f ) {
			rb.AddForce ( new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y)*0.5f);
		}
	}


	void InstantiateBall( ref GameObject gb, String name, Vector3 v, Color c ){
		Renderer r;
		gb = Instantiate(ball, v, Quaternion.identity) as GameObject; 
		gb.name = gb.tag = name;
		gb.transform.localScale = new Vector3 (0.0527f, 0.0527f, 0.0527f);
		r = gb.GetComponent<Renderer> ();
		r.material.color = c;
	}

	Vector3[] getPoints(){
		String[] splits;
		StreamReader sr = new StreamReader ("Assets/Scripts/test.txt");
		Vector3[] points = new Vector3[3];
		str = sr.ReadLine ();
		int i = 0;
		while (str != null) {
			splits = str.Split(' ');
			float x = float.Parse(splits[0]);
			float z = float.Parse(splits[1]);
			points[i++] = new Vector3(x,75f,z)*0.001f;
			str = sr.ReadLine();
		}

		for (i=0; i<3; i++) {
			Debug.Log("i = "+points[i]);
		}
		return points;
	}

	void OnCollisionEnter (Collision c){
		ContactPoint cp = c.contacts[0];
		if (c.gameObject.tag.Equals("white")) {
			empty = new GameObject("LineRenderer");
			empty.AddComponent<LineRenderer> ();
			LineRenderer lr = empty.GetComponent<LineRenderer> ();
			lr.SetVertexCount (2);
			lr.SetWidth (0.01f, 0.01f);
			lastPoint.y += 0.01f;
			actualPoint = cp.point;
			actualPoint.y += 0.01f;
			lr.SetPosition (0, lastPoint);
			lr.SetPosition (1, actualPoint);
			lastPoint = actualPoint;
		}
	}
}