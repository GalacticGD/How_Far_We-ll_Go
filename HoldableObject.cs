using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldableObject : MonoBehaviour
{
    public GameObject popUpPreFab;
    private GameObject popUpTxt;
    private PlayerController nearestPlayer;

    private Rigidbody2D rb;

    public LayerMask whatIsPlayer;
    public float popUpRadius;
    private bool inRange = false;
    private bool pickedUp = false;


    void Start()
    {
        popUpTxt = Instantiate(popUpPreFab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity, FindObjectOfType<Canvas>().transform);
        disablePopUp();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!pickedUp)
        {
            Collider2D playerCol = Physics2D.OverlapCircle(transform.position, popUpRadius, whatIsPlayer);
            if (playerCol != null)
            {
                inRange = true;
                nearestPlayer = playerCol.gameObject.GetComponent<PlayerController>();
                string txt = "'" + nearestPlayer.interact.ToString() + "' to Carry";
                enablePopUp(txt);
            } else
            {
                disablePopUp();
                inRange = false;
            }
        }

        
    }

    private void Update()
    {

        if (inRange && !pickedUp)
        {
            if (Input.GetKeyDown(nearestPlayer.interact))
            {
                inRange = false;
                pickUp(nearestPlayer.gameObject.transform);
            }
        }
        else if (pickedUp && Input.GetKeyDown(nearestPlayer.interact))
        {
            drop(nearestPlayer.transform);
        } 
    }


    public void pickUp(Transform player)
    {
        transform.position = new Vector2(player.position.x, player.position.y + 0.5f);
        transform.parent = player;
        rb.isKinematic = true;
        setPickedUp(true);
        player.gameObject.GetComponent<PlayerController>().SetCarrying(true);
    }

    public void drop(Transform player)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        transform.parent = null;
        rb.isKinematic = false;
        setPickedUp(false);
        player.gameObject.GetComponent<PlayerController>().SetCarrying(false);
        popUpTxt.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
    }

    public void enablePopUp(string txt)
    {
        popUpTxt.GetComponent<Text>().text = txt;
        popUpTxt.SetActive(true);
    }

    public void disablePopUp()
    {
        popUpTxt.SetActive(false);
    }

    public void setPickedUp(bool setPickedUp)
    {
        pickedUp = setPickedUp;
        if (setPickedUp == true)
        {
            disablePopUp();
        }
    }

}
