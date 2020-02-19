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
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;

    private Animator myAnimator;
    private GameObject myShrink;
    public RestartController restart;
    public WaitForSeconds await;

    //Shake Cam
    public float shakeAmount = 0.01f;
    public float shakeLength = 1.05f;

    //Audio
    public AudioSource shrinkSound;
    public AudioSource unshrinkSound;
    public AudioSource deathSound;
    public AudioSource jumpSound;
    public AudioSource theme;

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
            jumpSound.Play();
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
            unshrinkSound.Play();
            Debug.Log("Unshrunk Successful");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            myAnimator.SetTrigger("Death");
            CameraShakeController.shakeInstance.ShakeCam(shakeAmount, shakeLength);
            restart.Restart();
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            deathSound.Play();
            theme.PlayDelayed(1.25f);
            Debug.Log("Death Successful");
        }
        if (collision.gameObject.tag == "deathup" && myShrink == enabled)
        {
            myAnimator.SetTrigger("Death");
            CameraShakeController.shakeInstance.ShakeCam(shakeAmount, shakeLength);
            restart.Restart();
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            deathSound.Play();
            theme.PlayDelayed(1.25f);
            Debug.Log("Death While Shrunk Successful");
        }
        if (collision.gameObject.tag == "enemydeath")
        {
            CameraShakeController.shakeInstance.ShakeCam(shakeAmount, shakeLength);
            restart.Restart();
            myAnimator.SetTrigger("Death");
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            deathSound.Play();
            theme.PlayDelayed(1.25f);
            Debug.Log("Death by Enemy Successful");
        }
        if (collision.gameObject.tag == "portal")
        {
            Destroy(myShrink);
            myShrink.gameObject.transform.localScale = new Vector3(1, 1, 1);
            jumpForce = 17;
            theme.Stop();
            Debug.Log("Game Finished");
        }
    }
}
