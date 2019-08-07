using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 8;
    public float minSpeed = 4;

    private SpriteRenderer render;
    public Sprite hurted;
    public GameObject boom;//爆炸特效
    public GameObject score;//得分特效

    public AudioClip damaged;
    public AudioClip destroyed;
    public AudioClip birdcoll;

    public bool isPig = false;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);

        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdcoll);
            collision.transform.GetComponent<Bird>().Hurt();
        }

        if (collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();
        }
        else if(collision.relativeVelocity.magnitude > minSpeed)
        {
            render.sprite = hurted;
            AudioPlay(damaged);
        }
        
    }

    public void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pigs.Remove(this);
            GameManager._instance.IfLastPig();
        }
        Destroy(gameObject);

        AudioPlay(destroyed);

        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);

    }

    public void AudioPlay(AudioClip ac)
    {
        AudioSource.PlayClipAtPoint(ac, transform.position);
    }
}
