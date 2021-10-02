using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    private float dirX, dirY, moveSpeed;
    private float gravityMin = 0, gravityMax = 12, currentGravity = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        dirY = Input.GetAxisRaw("Vertical") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchGravity();
        }


    }

    void StartClimb()
    {
        
    }

    void SwitchGravity()
    {
        Debug.Log("RAn");

        currentGravity = currentGravity == gravityMin ? gravityMax : gravityMin;
        rb.gravityScale = currentGravity;
        /*
        if(currentGravity == gravityMin)
       
            currentGravity = gravityMin;
        }
             rb.gravityScale  = gravityMax; //to substract

        else if (currentGravity == gravityMax)
            rb.gravityScale = gravityMin; //to substract

    */
    }

    void Jump()
    {
       
    }


    public void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY);
    }
}
