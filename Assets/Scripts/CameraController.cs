using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController myPlayer;
    private Vector3 playerPosition;
    private float distanceToMove;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerController>();
        playerPosition = myPlayer.transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMove = myPlayer.transform.position.x - playerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        playerPosition = myPlayer.transform.position;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cam.orthographicSize = 3.5f;
            transform.position = new Vector3((transform.position.x + distanceToMove) - 3, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            cam.orthographicSize = 5.0f;
            transform.position = new Vector3((transform.position.x + distanceToMove) + 3, transform.position.y, transform.position.z);
        }
    }
}
