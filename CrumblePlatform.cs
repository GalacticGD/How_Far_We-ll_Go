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
            anim.SetTrigger("Crumble");
            //StartCoroutine("");
        }
    }

    //public IEnumerator Platform
}
