using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	void Update () {
		Ray a = new Ray( new Vector3((float)1.251, (float)0.075, (float)0.935), transform.forward );
		Ray b;
		RaycastHit hit;
		
		if( Deflect( a, out b, out hit )  ){
			Debug.DrawLine( a.origin, hit.point );
			Debug.DrawLine( b.origin, b.origin + 3 * b.direction );
		}
	}
	
	bool Deflect( Ray ray, out Ray deflected, out RaycastHit hit ) {
		
		if( Physics.Raycast(ray, out hit) ) {
			Vector3 normal = hit.normal;
			Vector3 deflect = Vector3.Reflect( ray.direction, normal );
			
			deflected = new Ray( hit.point, deflect );
			return true;
		}
		
		deflected = new Ray( Vector3.zero, Vector3.zero );
		return false;
	}
}