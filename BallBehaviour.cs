using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallBehaviour : MonoBehaviour {

	//private Rigidbody rb;
	public int force;
	public int linesNumber;
	private Vector3 initialPoint;
	private Vector3 lastPoint;
	private Vector3 actualPoint;
	private Vector3 reflection;
	private Vector3 mousePosition;
	private GameObject[] empty;
	public GameObject publicCue;
	private GameObject cue;

	// Use this for initialization
	void Start () {
		linesNumber = 3;
		linesNumber ++;
		if( tag.Equals("white") )
			Initialize ();
		force = 3;
	}

	void Update () {
		Vector3 mousePosition = new Vector3( Input.mousePosition.x, 0, Input.mousePosition.y) ;
		//cueBehaviour ();
		GameObject ball = GameObject.FindGameObjectWithTag ("white");
		Rigidbody rb = ball.GetComponent<Rigidbody> ();
		if (tag.Equals ("white") && rb.velocity.magnitude < 0.01f) {
			createLines ();
		}
		if (tag.Equals ("white") && rb.velocity.magnitude > 0.01f) {
			destroyLines ();
			Initialize ();
		}

	}

	void destroyLines(){
		for (int i=0; i<linesNumber; i++) {
			Destroy( empty[i] );
		}
	}

	void createLines (){

		RaycastHit hit;
		RaycastHit hit2;
		RaycastHit hit3;


		RaycastHit[] hits = new RaycastHit[linesNumber];
		Vector3[] initialPoints = new Vector3[linesNumber];
		Vector3[] finalPoints = new Vector3[linesNumber];
		Vector3[] rayDirections = new Vector3[linesNumber];

		Vector3 mousePosition = new Vector3( Input.mousePosition.x, 0f, Input.mousePosition.y) ;
		mousePosition.x = mousePosition.x - transform.position.x;
		mousePosition.z = mousePosition.z - transform.position.z;
		Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
		Debug.Log ( mouse );
		Debug.Log ( "Ancho = " + Screen.width );
		Debug.Log ( "Alto = " + Screen.height );


		/*
		Vector3 screenHeight = new Vector3( (float)Screen.width, 0f, (float)Screen.height );
		Vector3 zeros = new Vector3( 0f, 0f, 0f );
		mousePosition = crossMultiplication( mousePosition, zeros, screenHeight );
		Debug.Log("Screen = " + screenHeight);
		Debug.Log (mousePosition);
		*/

		Ray rayoInicial = Camera.main.ScreenPointToRay(Input.mousePosition);
		initialPoints [0] = transform.position;
		rayDirections [0].x = rayoInicial.direction.x;
		rayDirections [0].y = 0f;
		rayDirections [0].z = rayoInicial.direction.z;
		Physics.SphereCast (initialPoints [0], 0.0527f, rayDirections[0], out hits [0]);
		finalPoints[0] = hits[0].point;
		for (int i=1; i<linesNumber; i++) {
			initialPoints[i] = finalPoints[i-1];
			rayDirections[i] = Vector3.Reflect( rayDirections[i-1] , hits[i-1].normal);
			rayDirections[i].y = 0f;
			if( Physics.SphereCast (initialPoints [i], 0.0527f, rayDirections [i], out hits [i]) ){
				finalPoints[i] = hits[i].point;
				finalPoints[i].y = 0.075f;
			}
		}

		drawLines (initialPoints, finalPoints);

		//drawLine( empty[0].GetComponent<LineRenderer>(), transform.position, mousePosition );
		/*
		initialPoint = transform.position;
		lastPoint = mousePosition;
		if ( Physics.Raycast (initialPoint, lastPoint, out hit ) ) {
			lastPoint = hit.point;
			drawLine( empty[1].GetComponent<LineRenderer>(), initialPoint, lastPoint );
			lastPoint = Vector3.Reflect( initialPoint , hit.normal);
			lastPoint.y = 0f;
			initialPoint = hit.point;
			if( Physics.Raycast ( initialPoint, lastPoint, out hit2 ) ){
				lastPoint = hit2.point;
				drawLine( empty[2].GetComponent<LineRenderer>() , initialPoint, lastPoint );
				lastPoint = Vector3.Reflect( initialPoint , hit2.normal);
				lastPoint.y = 0f;
				initialPoint = hit2.point;
				if( Physics.Raycast (initialPoint, lastPoint, out hit3) ){
					lastPoint = hit3.point;
					drawLine( empty[3].GetComponent<LineRenderer>() , initialPoint, lastPoint);
				}
			}
		}
		*/
	}

	/*
	void OnMouseDown() {
		if (tag.Equals ("white")) {
			Rigidbody rb = GetComponent<Rigidbody> ();
			rb.AddForce (new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y) * 3);
			Debug.Log ("wolax");
		}
	}
	*/

	void drawLines( Vector3[] initialPoints, Vector3[] finalPoints ){
		for (int i=0; i<linesNumber; i++) {
			drawLine( empty[i].GetComponent<LineRenderer>(), initialPoints[i], finalPoints[i] );
		}
	}

	Vector3 crossMultiplication(Vector3 point, Vector3 downLeft, Vector3 upRight ){

		Vector3 resta = upRight - downLeft;
		Vector3 tableUpRight = new Vector3 ( 1.27f, 0f, 2.79f );
		Debug.Log ("Point = " + point);
		Vector3 temp = new Vector3(point.x*resta.x/tableUpRight.x, 0f ,point.z*resta.z/tableUpRight.z);
		Debug.Log ("temp = " + temp);
		return temp;
	}

	void drawLine( LineRenderer lr, Vector3 origin, Vector3 destination ){
		lr.SetPosition (0, origin);
		lr.SetPosition (1, destination);
	}

	void Initialize (){

		//cue = Instantiate ( publicCue, new Vector3(0,0,0), Quaternion.identity) as GameObject;

		empty = new GameObject[linesNumber];
		for (int i=0; i<linesNumber; i++) {
			empty[i] = new GameObject("guideLine");
			empty[i].AddComponent<LineRenderer> ();
			empty[i].tag = "guideLine";
			LineRenderer lr = empty[i].GetComponent<LineRenderer> ();
			lr.SetVertexCount (2);
			lr.SetWidth (0.005f, 0.005f);
		}
	}
}