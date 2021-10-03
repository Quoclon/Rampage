using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject == null)
            return;

        if (collision.gameObject.tag == "Breakable")
        {
            if(collision.GetComponent<BreakableBlockScript>() != null)
                collision.GetComponent<BreakableBlockScript>().BreakBrick();
            //collision.GetComponentInParent<GameObject>().SetActive(false);
        }
    }

    /*
    private void FixedUpdate()
    {

        float rayCastDrawDistance = 2f;

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, rayCastDrawDistance);
        Debug.DrawRay(transform.position, Vector2.right * rayCastDrawDistance, Color.green);

        // If it hits something...
        if (hit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            //float heightError = floatHeight - distance;

            Debug.Log(distance);
            Debug.Log(hit.collider.tag);

            // The force is proportional to the height error, but we remove a part of it
            // according to the object's speed.
            //float force = liftForce * heightError - rb2D.velocity.y * damping;

            // Apply the force to the rigidbody.
            //rb2D.AddForce(Vector3.up * force);
        }
    }
    */
}
