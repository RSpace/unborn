using UnityEngine;
using System.Collections;

public class ControllerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().GetComponent<Material>().color = Color.blue;    	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
