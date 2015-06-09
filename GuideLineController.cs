using UnityEngine;
using System.Collections;

public class GuideLineController : MonoBehaviour {

	private LineRenderer lineRenderer;
	public Transform yellow;
	public Transform white;
	

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetColors (Color.blue, Color.blue);
		lineRenderer.SetPosition (0, yellow.position);
		lineRenderer.SetPosition (1, white.position);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
