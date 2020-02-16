using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public float cameraShake = 0.7f;
    public float cameraDuration = 1.0f;
    public Transform camera;
    public PlayerController myPlayer;
    public float slowDownAmt = 1.0f;
    public bool shakeEnabled = false;

    Vector3 startPos;
    float initialDuration;

    void Start()
    {
        //camera = Camera.main.transform;
        startPos = camera.localPosition;
        initialDuration = cameraDuration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            if(cameraDuration > 0)
            {
                shakeEnabled = true;
                myPlayer.transform.position = startPos + Random.insideUnitSphere * cameraShake;
                cameraDuration -= Time.deltaTime * slowDownAmt;
            }
            else
            {
                shakeEnabled = false;
                cameraDuration = initialDuration;
                camera.localPosition = startPos;
            }
        }
    }
}
