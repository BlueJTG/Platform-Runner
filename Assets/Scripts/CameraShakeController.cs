using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public static CameraShakeController shakeInstance;

    public PlayerController myPlayer;
    private Vector3 playerPosition;
    private float distanceToMove;
    public Camera mainCam;
    float shakeAmount = 0;

    private void Start()
    {
        shakeInstance = this;
        if (mainCam == null)
            mainCam = Camera.main;
    }

    private void Update()
    {
        myPlayer = FindObjectOfType<PlayerController>();
        playerPosition = myPlayer.transform.position;

        if (Input.GetKeyDown(KeyCode.T))
        {
            ShakeCam(0.2f, 0.1f);
        }
    }

    public void ShakeCam(float amt, float duration)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", duration);
    }

    public void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 initialPos = myPlayer.transform.position;
            float shakeAmtX = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeAmtY = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeAmtZ = Random.value * shakeAmount * 2 - shakeAmount;
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
   