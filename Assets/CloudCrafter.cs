using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour {
	//set in inspector
	public int numClouds = 40; 			// # clouds to make
	public GameObject[] cloudPrefabs;	// pfabs for clouds
	public Vector3 cloudPosMin;			// min pos for cloud
	public Vector3 cloudPosMax; 		// max pos for cloud
	public float scaleMin = 1; 			// min scale for clouds
	public float scaleMax = 5;			//max scale for clouds
	public float cloudSpeedMult = 0.5f;
	public bool ________________________________________;
	// set dynamicly
	public GameObject[] cloudInstances;


	void Awake(){
		// make array for cloud instances;
		cloudInstances = new GameObject[numClouds];
		// find cloud anchor
		GameObject anchor = GameObject.Find ("CloudAnchor");
		// iterate and make clouds
		GameObject cloud;
		for (int i =0; i<numClouds; i++) {
		// pick# btwn 0 & cloudprefabs.length-1
			int prefabNum = Random.Range(0,cloudPrefabs.Length);
			// make instance
			cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range(cloudPosMin.x,cloudPosMax.x);
			cPos.y = Random.Range(cloudPosMin.y,cloudPosMax.y);
			//scale
			float scaleU = Random.value;
			float scaleVal = Mathf.Lerp(scaleMin,scaleMax,scaleU);
			cPos.y = Mathf.Lerp(cloudPosMin.y,cPos.y,scaleU);
			cPos.z= 100 - 90*scaleU;
			cloud.transform.position = cPos;
			cloud.transform.localScale = Vector3.one * scaleVal;
			cloud.transform.parent = anchor.transform;
			cloudInstances[i]=cloud;
		
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject cloud in cloudInstances) {
		
			float scaleVal = cloud.transform.localScale.x;
			Vector3 cPos = cloud.transform.position;

			cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
			if (cPos.x <= cloudPosMin.x){
				cPos.x = cloudPosMax.x;
			}
			cloud.transform.position=cPos;
		}
	
	}
}
