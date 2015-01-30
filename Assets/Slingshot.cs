using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	static public Slingshot S;
	//public GUIText ShotGT;
	// fields set in inspector

	public GameObject prefabProjectile;
	public float velocityMult = 10f;
	public bool ________________________;
	//fields set dynamicly
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;
	public GameObject launchPoint;
	public int shotNum;

	void Awake(){
		S = this;
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter(){
		//print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);

	}

	void OnMouseExit(){
		//print ("Slingshot:OnMouseExit()");
		launchPoint.SetActive (false);

	}
	void OnMouseDown(){
		// mouse button pressed while over slingshot
		aimingMode = true;
		//instanciate a projectile
		projectile = Instantiate (prefabProjectile) as GameObject;
		//start it @ launchpt
		projectile.transform.position = launchPos;
		// set to iskinematic
		projectile.rigidbody.isKinematic = true;
	}

	// Use this for initialization
	void Start () {


		shotNum = 0;
		//GameObject shotGO = GameObject.Find ("ShotCnt");
		//ShotGT = shotGO.GetComponent<GUIText> ();
		//ShotGT.text = "Shot: 0";


	
	}
	
	// Update is called once per frame
	void Update () {
		// if in aiming mode don't run this
		if (!aimingMode) return;
				
		// get current mouse position
		Vector3 mousePos2D = Input.mousePosition;
		// convert to 3d
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);
		/// find delta from launch pt
		Vector3 mouseDelta = mousePos3D - launchPos;
		// limit to radius of slingshot shhere collider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;

		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
			//print ("lllll");
				
		}
		// move projectile
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp(0)) {
			shotNum++;

			//ShotGT.text= "Shot: " + shotNum;
			aimingMode = false;
			projectile.rigidbody.isKinematic = false;
			projectile.rigidbody.velocity = -mouseDelta * velocityMult;


			//print (projectile.rigidbody.velocity);
			followCam.S.poi = projectile;
			projectile=  null;

		
		}

	


	
	}

}
