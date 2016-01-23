using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

    private Animator anim;
    // Use this for initialization'
    public Transform MoveTo;
    public float speedmodifier = 5f;
    private float speed = 0f;// Vector3.zero;
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("No Animator found");
        transform.forward = (Vector3.RotateTowards(transform.forward, (MoveTo.position - transform.position).normalized,Mathf.PI,Mathf.PI) );
	}
	
	// Update is called once per frame
	void Update () {
        speed = Mathf.Lerp(0f, 1f, speed + Time.deltaTime);
        Debug.Log(speed);
        transform.position = Vector3.MoveTowards(transform.position, MoveTo.position, speed * speedmodifier * Time.deltaTime); //Vector3.SmoothDamp(transform.position, MoveTo.position, ref speed,0.8f);
        anim.SetFloat("speed", speed);

    }
}
