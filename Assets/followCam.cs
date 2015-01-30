using UnityEngine;
using System.Collections;

public class followCam : MonoBehaviour {
	static public followCam S; // a singleton
	// fields set in inspector
	public float easing = 0.05f;
	public Vector2 minXY;
	public bool __________________;

	// dynamic fields
	public GameObject poi; //Point Of Intrest
	public float camZ; // desired z pos of camea

	void Awake(){
		S = this;
		camZ = this.transform.position.z;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 destination;
		// if no poi return to 0.0.0
		if (poi == null) {
						destination = Vector3.zero;
				} else {
			destination = poi.transform.position;
			// if oi is a projectile see if it is at rest
			if (poi.tag == "Projectile"){
				if (poi.rigidbody.IsSleeping()||poi.transform.position.y < -20){ 
					// added the position part to prevent camera from following objects that are falling endlessly (however rare this may be)
					poi = null;
					return;
				}
			}
		
		}




		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);
		// interpolate from current cam pos to destination
		destination = Vector3.Lerp(transform.position, destination, easing);

		destination.z = camZ;

		transform.position = destination;
		// set ortho size to keep ground in view
		this.camera.orthographicSize = destination.y + 10;
	
	}
}
