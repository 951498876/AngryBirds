using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    private List<Pig> targets = new List<Pig>();
    private bool ifBoomed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        ifBoomed = true;
        print("Black show skill");
        if (targets.Count > 0 && targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Dead();
            }
        }
        OnClear();
    }

    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.CleanTrail();
    }

    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        if (!ifBoomed)
        {
            Instantiate(boom, transform.position, Quaternion.identity);
        }

        GameManager._instance.NextBird();
    }
}
