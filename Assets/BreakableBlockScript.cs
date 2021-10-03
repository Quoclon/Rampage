using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockScript : MonoBehaviour
{

    public BlockScript blockScript;


    // Start is called before the first frame update
    void Start()
    {
        blockScript = GetComponentInParent<BlockScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakBrick()
    {
        //parentBlockScript.gameObject.transform.parent.gameObject.SetActive(false);
        blockScript.windowOpen.SetActive(false);
        blockScript.windowClosed.SetActive(false);
        blockScript.Brick.SetActive(false);
        blockScript.BrickBroken.SetActive(true);
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "fist")
        {
            Debug.Log("Fisted");
            parentBlockScript.blockState++;
            Debug.Log(parentBlockScript.blockState);
        }
    }
    */

}
