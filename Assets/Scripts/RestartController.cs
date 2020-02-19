using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    public Vector3 startPosition;

    public PlayerController myPlayer;
    private Vector3 playerStart;

    // Start is called before the first frame update
    void Awake()
    {
        playerStart = myPlayer.transform.position;
    }

    public void Restart()
    {     
        StartCoroutine("RestartGame");
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.05f);
        myPlayer.transform.position = playerStart;
        myPlayer.gameObject.SetActive(true);
    }

    
}
