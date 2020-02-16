using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    //public Transform platform;
    public Vector3 startPosition;

    public PlayerController myPlayer;
    private Vector3 playerStart;

    // Start is called before the first frame update
    void Start()
    {
        //startPosition = platform.position;
        playerStart = myPlayer.transform.position;
    }

    public void Restart()
    {
        StartCoroutine("RestartGame");
    }

    public IEnumerator RestartGame()
    {
        myPlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        myPlayer.transform.position = playerStart;
        myPlayer.gameObject.SetActive(true);
    }
}
