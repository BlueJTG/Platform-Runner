using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour  
{ 
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public bool death;
    public LayerMask whatIsGround;
    private CameraController cam;
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private GameObject myShrink;
    public RestartController restart;
    public WaitForSeconds await;
    public AudioSource shrinkSound;
    public AudioSource deathSound;
    private GameObject cameraStop;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        myShrink = GameObject.Find("Player");
        cam = GetComponent<CameraController>();
        cameraStop = GameObject.Find("CameraStop");
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
            jumpForce = 9;
            shrinkSound.Play();
            Debug.Log("Shrink Successful");

        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            Debug.Log("Unshrunk Successful");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            //cam.enabled = !cam.enabled;
            myAnimator.SetTrigger("Death");
            restart.Restart();
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            deathSound.Play();
            Debug.Log("Death Successful");
        }
        if (collision.gameObject.tag == "deathup" && myShrink == enabled)
        {
            myAnimator.SetTrigger("Death");
            restart.Restart();
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            deathSound.Play();
            Debug.Log("Death While Shrunk Successful");
        }
        if (collision.gameObject.tag == "enemydeath")
        {
            //cam.enabled = !cam.enabled;
            myAnimator.SetTrigger("Death");
            restart.Restart();
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            deathSound.Play();
            Debug.Log("Death by Enemy Successful");
        }
        if (collision.gameObject.tag == "portal")
        {
            Destroy(myShrink);
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            Debug.Log("Game Finished");
        }
    }
}
