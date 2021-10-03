using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public  float dirX, dirY;
    public float jumpForce, moveSpeed;
    public float climbingGravity, defaultGravity, currentGravity;

    public bool facingRight = true;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public bool isTouchingFront;
    public Transform frontCheck;
    public LayerMask whatIsBuilding;

    public bool isClimbing;
    public GameObject lastClimbedBlock;

    public bool isJumping;

    public bool wallSliding;
    public float wallSlidingSpeed;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGravity = defaultGravity;
    }

    // Update is called once per frame
    void Update()
    {    

        //Check if Grounded and Touching (using a "checkRadious" ~)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isJumping = isGrounded ? false : true;

        //Jump if grounded, but not touching building side
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping && !isClimbing)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpForce);


        //Movement Horizontal - Locked if jumping
        if (!isJumping)
        {
            float input = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

            //Flip if pressing left or right
            if (input > 0 && !facingRight && !isClimbing)
                Flip();
            else if (input < 0 && facingRight && !isClimbing)
                Flip();
        }


        //Check for building and climb if possible
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsBuilding);
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && isTouchingFront)
        {
            Collider2D[] buildingBricks = Physics2D.OverlapCircleAll(frontCheck.position, checkRadius, whatIsBuilding);
           
            for (int i = 0; i < buildingBricks.Length; i++)
            {
                if (transform.localScale.x != buildingBricks[i].transform.parent.localScale.x)
                {
                    transform.position = new Vector2(buildingBricks[i].transform.position.x - (transform.localScale.x/2), transform.position.y);
                    isClimbing = true;
                }
            }
        }

        if (isClimbing)
        {
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsBuilding);
            if (isTouchingFront)
            {
                Collider2D[] buildingBricks = Physics2D.OverlapCircleAll(frontCheck.position, checkRadius, whatIsBuilding);
                for (int i = 0; i < buildingBricks.Length; i++)
                {
                    lastClimbedBlock = buildingBricks[i].transform.parent.gameObject;

                    /*
                    if (buildingBricks[i].transform.parent.GetComponent<BlockScript>().isTopBlock)
                    {
                        Transform topBlock = buildingBricks[i].transform.parent.gameObject.transform;
                        transform.position = new Vector2(topBlock.position.x + 1, topBlock.position.y + transform.localScale.y);
                        lastClimbedBlock = buildingBricks[i].transform.parent.gameObject;
                    }
                    */
                }
            }
        }else
        {
            lastClimbedBlock = null;
        }
        


          if (isClimbing)
        {
            currentGravity = climbingGravity;
            float moveX = 0;
            float moveY = 0;
            moveY = Input.GetAxisRaw("Vertical") * (moveSpeed / 2);

            if (isTouchingFront)
                moveY = Input.GetAxisRaw("Vertical") * (moveSpeed / 2);

            if (!isTouchingFront)
            {
                if(Input.GetAxisRaw("Vertical") > 0.1f)
                {
                    isClimbing = false;
                    if(lastClimbedBlock != null)
                    {
                        Transform topBlock = lastClimbedBlock.transform;
                        moveX = 0;
                        moveY = 0;
                        transform.position = new Vector2(topBlock.position.x, topBlock.position.y + transform.localScale.y / 2);
                    }
                }
                else
                {
                    moveY = Mathf.Clamp(Input.GetAxisRaw("Vertical") * (moveSpeed / 2), float.MinValue, 0f);
                }
            }

            rb.velocity = new Vector2(moveX, moveY);
        }

        if (isClimbing && Input.GetKeyDown(KeyCode.Space))
        {
            isClimbing = !isClimbing;
            rb.velocity = new Vector2(-transform.localScale.x * 1.5f, rb.velocity.y + jumpForce / 4);
            isJumping = true;
        }

        if (isClimbing && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && isGrounded)
            isClimbing = !isClimbing;

        //Set Gravity
        currentGravity = isClimbing ? climbingGravity : defaultGravity;
        rb.gravityScale = currentGravity;  
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(frontCheck.position, checkRadius);
    }

}
