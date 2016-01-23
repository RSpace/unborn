using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class MotherTrigger : MonoBehaviour {

    public AudioClip[] sounds;
    // Use this for initialization
    private int hp;
    AudioSource audio;
    void Start()
    {
        hp = sounds.Length;
        audio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Animator agentAnim = (other.gameObject.GetComponent<Animator>());
        agentAnim.SetTrigger("attack");
       
        TakeDamage();
    }
     
    private void TakeDamage()
    {
         
       audio.clip = sounds[sounds.Length - hp];
        audio.Play();
        hp--;
        if (hp == 0)
            Die();
    }
    private void Die()
    {

    }
}
