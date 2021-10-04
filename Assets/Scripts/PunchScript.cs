using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    //~You removed the tag "Fist" for player bullet collsion, may be needed?

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null)
            return;

        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            return;
        }

        if (collision.gameObject.tag == "Breakable")
        {
            if(collision.GetComponent<BreakableBlockScript>() != null)
                collision.GetComponent<BreakableBlockScript>().BreakBrick();
            gameObject.SetActive(false);
            return;
        }
    }
}
