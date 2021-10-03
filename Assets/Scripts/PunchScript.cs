using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);

        if (collision.gameObject == null)
            return;

        if (collision.gameObject.tag == "Breakable")
        {
            if(collision.GetComponent<BreakableBlockScript>() != null)
                collision.GetComponent<BreakableBlockScript>().BreakBrick();
            gameObject.SetActive(false);
        }
    }
}
