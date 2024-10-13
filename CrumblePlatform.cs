using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblePlatform : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("PlatformCrumble");
        }
    }

    public IEnumerator PlatformCrumble()
    {
        anim.SetBool("Crumble", true);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        yield return new WaitForSeconds(1f);
        collider.enabled = false;
        yield return new WaitForSeconds(2f);
        collider.enabled = true;
        anim.SetBool("Crumble", false);
    }
}
