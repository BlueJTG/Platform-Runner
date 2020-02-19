using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public PlayerController myPlayer;
    private Vector3 playerPosition;
    private float distanceToMove;
    public Camera mainCam;
    float shakeAmt = 0;

    private void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    private void Update()
    {
        myPlayer = FindObjectOfType<PlayerController>();
        playerPosition = myPlayer.transform.position;

        if (Input.GetKeyDown(KeyCode.T))
        {
            ShakeCam(0.2f, 0.2f);
        }
    }

    public void ShakeCam(float amt, float duration)
    {
        shakeAmt = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", duration);
    }

    public void BeginShake()
    {
        if (shakeAmt > 0)
        {
            Vector3 initialPos = myPlayer.transform.position;
            float shakeAmtX = Random.value * shakeAmt * 2 - shakeAmt;
            float shakeAmtY = Random.value * shakeAmt * 2 - shakeAmt;
            float shakeAmtZ = Random.value * shakeAmt * 2 - shakeAmt;
            initialPos.x -= shakeAmtX;
            initialPos.y -= shakeAmtY;
            initialPos.z = shakeAmtZ;


            myPlayer.transform.position = initialPos;

        }
    }

    public void StopShake()
    {
        CancelInvoke("BeginShake");
        distanceToMove = myPlayer.transform.position.x - playerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        playerPosition = myPlayer.transform.position;
    }
}
   