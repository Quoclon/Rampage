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
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public float footCheckRadius;

    public Transform leftGroundCheck;
    public bool leftFootGrounded;

    public Transform rightGroundCheck;
    public bool rightFootGrounded;

    public bool isTouchingFront;
    public Transform frontCheck;
    public float checkRadius;
    public LayerMask whatIsBuilding;

    public bool isClimbing;
    public GameObject lastClimbedBlock;

    public bool isJumping;

    public bool wallSliding;
    public float wallSlidingSpeed;

    public GameObject punchGameObject;
    public float punchGOactiveTimer;
    public float punchGOactiveTimerTotal;

    public float punchTimer;
    public float punchTimerTotal;
       
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGravity = defaultGravity;
        punchTimer = punchTimerTotal;
        punchGOactiveTimer = punchGOactiveTimerTotal;
    }

    public void HandlePunch()
    {
        if(punchTimer >= 0)
            punchTimer -= Time.deltaTime;

        if(punchGameObject.activeInHierarchy)
            punchGOactiveTimer -= Time.deltaTime;

        if(punchGOactiveTimer <= 0)
        {
            punchGameObject.SetActive(false);
            punchGOactiveTimer = punchGOactiveTimerTotal;
        }

  
        if (Input.GetKeyDown(KeyCode.H) && punchTimer <= 0.2f)
        {
            punchGameObject.SetActive(true);
            punchTimer = punchTimerTotal;
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {


        HandlePunch();


        //Check if Grounded and Touching (using a "checkRadious" ~)
        leftFootGrounded = Physics2D.OverlapCircle(leftGroundCheck.position, groundCheckRadius, whatIsGround);
        rightFootGrounded = Physics2D.OverlapCircle(rightGroundCheck.position, groundCheckRadius, whatIsGround);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isJumping = isGrounded ? false : true;

        /*
        if (isGrounded && (leftFootGrounded || rightFootGrounded))
            isJumping = false;
        else
            isJumping = true;
        */
   
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

        
        //Climbing Down from Top
        if (isGrounded && (!leftFootGrounded || !rightFootGrounded)){
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                Collider2D[] buildingBricks = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsBuilding);
                for (int i = 0; i < buildingBricks.Length; i++)
                {
                    lastClimbedBlock = buildingBricks[i].transform.parent.gameObject;

                    Debug.Log(lastClimbedBlock.name);
                    Debug.Log(lastClimbedBlock.transform.localScale.x);

                    if(transform.localScale.x == lastClimbedBlock.transform.localScale.x)
                    {
                        transform.position = new Vector2(lastClimbedBlock.transform.position.x + transform.localScale.x, lastClimbedBlock.transform.position.y);
                        Flip();
                    }else
                    {
                        transform.position = new Vector2(lastClimbedBlock.transform.position.x - transform.localScale.x, lastClimbedBlock.transform.position.y);
                    }


                }

                isClimbing = true;
            }
         
        }
        

        //Check for building and climb if possibled
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
        

        //Check for last climbed block on each update
        if (isClimbing)
        {
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsBuilding);
            if (isTouchingFront)
            {
                Collider2D[] buildingBricks = Physics2D.OverlapCircleAll(frontCheck.position, checkRadius, whatIsBuilding);
                for (int i = 0; i < buildingBricks.Length; i++)
                {
                    lastClimbedBlock = buildingBricks[i].transform.parent.gameObject;
                }
            }
        }else
        {
            lastClimbedBlock = null;
        }
        


        if (isClimbing)
        {
            //Debug.Log("isClimbing");
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

        //Set Gravity
        currentGravity = isClimbing ? climbingGravity : defaultGravity;
        rb.gravityScale = currentGravity;

        //~FIX - this relases you at bottom of climb, but breaks getting down from roof  
        /*
        if (isClimbing && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && isGrounded)
            isClimbing = !isClimbing;
        */
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

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(rightGroundCheck.position, footCheckRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(leftGroundCheck.position, footCheckRadius);
    }

}
