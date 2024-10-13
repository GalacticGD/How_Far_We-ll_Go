using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite open;
    //[System.NonSerialized]
    public bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Holdable"))
        {
            if (collision.gameObject.GetComponent<HoldableObject>().key)
            {
                GetComponent<SpriteRenderer>().sprite = open;
                isOpen = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
