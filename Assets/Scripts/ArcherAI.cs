using UnityEngine;
using System.Collections;

public class ArcherAI : MonoBehaviour {

    private Animator anim;
    // Use this for initialization'
    private Transform MoveTo;
    public float speedmodifier = 5f;
    private float speed = 0f;// Vector3.zero;
    public GameObject m_arrow;
	void Start () {
        MoveTo = GameObject.Find("mother").transform;

        anim = gameObject.GetComponent<Animator>();
        if (anim == null ) 
            Debug.LogError("No Animator found");
        transform.forward = (Vector3.RotateTowards(transform.forward, (MoveTo.position - transform.position).normalized,Mathf.PI,Mathf.PI));
	}
	
	// Update is called once per frame
/*	void FixedUpdate () {
        speed = Mathf.Lerp(0f, 1f, speed + Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, MoveTo.position, speed * speedmodifier * Time.deltaTime); //Vector3.SmoothDamp(transform.position, MoveTo.position, ref speed,0.8f);
        anim.SetFloat("speed", speed);

    }*/

    public void Shoot()
    {
        GameObject arrow = GameObject.Instantiate(m_arrow);
        arrow.GetComponent<Arrow>().enabled = true;
    }

  


}
