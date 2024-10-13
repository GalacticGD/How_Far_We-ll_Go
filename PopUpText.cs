using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    private LayerMask player;
    public bool kikiSpecific;
    private Text txt;
    public string action;
    private Collider2D nearestPlayer;

    private void Start()
    {
        txt = GetComponent<Text>();
        txt.enabled = false;
        player = LayerMask.GetMask("Player");
    }

    
    void FixedUpdate()
    {
        if (nearestPlayer != null)
        {
            nearestPlayer.GetComponent<PlayerController>().SetAvailableAction(null);
            nearestPlayer = null;
        }

        nearestPlayer = Physics2D.OverlapCircle(transform.position, 1.5f, player);
        if (nearestPlayer != null)
        {
            PlayerController playerScript = nearestPlayer.GetComponent<PlayerController>();
            if (kikiSpecific && playerScript.bouba == false)
            {
                txt.enabled = true;
                playerScript.SetAvailableAction(action);
            }
            
        } else
        {
            txt.enabled = false;
        }
    }
}
