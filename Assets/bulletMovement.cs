using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTimer;


    Rigidbody2D rb;
    Vector2 moveDirection;
    PlayerMovement target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMovement>(); //???
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;

        if (moveDirection.x <= 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }
}
