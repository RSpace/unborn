using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {


    public Transform[] spawnPoints;
    public Transform mother;

    public GameObject Agent0Prefab;

    public GameObject[] ActiveAgents;
	// Use this for initialization
	void Start () {
        StartCoroutine("Spawn");
	}

    IEnumerator Spawn()
    {
        int lvl = 1;
        yield return new WaitForSeconds(15f);
        for(int i = 0; i<500; i++)
        {
            Instantiate(Agent0Prefab, spawnPoints[Random.Range(0,spawnPoints.Length)].position, spawnPoints[0].rotation);
            yield return new WaitForSeconds(Random.Range(3f,10f));
        }
        yield return 0; 

    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
