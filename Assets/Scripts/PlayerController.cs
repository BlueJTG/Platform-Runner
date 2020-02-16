using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour  
{ 
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private GameObject myShrink;
    public RestartController restart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        myShrink = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //zoomCam.gameObject = 
            myShrink.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jumpForce -= 5;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            restart.Restart();
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
        }
    }
}
