using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class MotherTrigger : MonoBehaviour
{

    public AudioClip[] sounds;
    public GameObject dieEffectPrefab;

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
        if (other.tag != "enemy")
            return;

        Animator agentAnim = (other.gameObject.GetComponent<Animator>());
        if (agentAnim != null)
            agentAnim.SetTrigger("attack");

        TakeDamage();
    }

    private void TakeDamage()
    {
        if (hp <= 0)
            return;

        audio.clip = sounds[sounds.Length - hp];
        audio.Play();
        hp--;
        if (hp == 0)
            Die();
    }

    private void Die()
    {
        var dieEffect = GameObject.Instantiate(dieEffectPrefab);
        dieEffect.transform.position = gameObject.transform.position;
        StartCoroutine(WaitAndPrint());
        //Destroy (gameObject);			
    }


    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(3);
        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.red, 3);
        yield return new WaitForSeconds(3);
        Application.LoadLevel(0);
    }
}