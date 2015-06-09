using UnityEngine;
using System.Collections;

public class GuideLine : MonoBehaviour {

	private LineRenderer lineRenderer;
	private GameObject yellow;
	private GameObject white;
	
	
	// Use this for initialization
	void makeGuideLine () {
		yellow = GameObject.FindGameObjectWithTag("yellow");
		white = GameObject.FindGameObjectWithTag("white");

		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
		lineRenderer.SetColors (Color.black, Color.black);
		lineRenderer.SetWidth (0.25f, 0.25f);
		lineRenderer.SetPosition (0, yellow.transform.position);
		lineRenderer.SetPosition (1, white.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
