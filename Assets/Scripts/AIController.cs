using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {


    public Transform[] spawnPoints;

    public GameObject Agent0Prefab;

    public GameObject[] ActiveAgents;
	// Use this for initialization
	void Start () {
        StartCoroutine("Spawn");
	}

    IEnumerator Spawn()
    {
        for(int i = 0; i<5; i++)
        {
            Debug.Log("Spawn");
            Instantiate(Agent0Prefab, spawnPoints[0].position, spawnPoints[0].rotation);
            yield return new WaitForSeconds(10.0f);
        }
        yield return 0; 

    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
