using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Player;
    PlatformSpawner platformSpawner;
    ObstacleSpawner obstacleSpawner;
    private int timer;

    // Start is called before the first frame update
    void Start()
    {
        timer=0;
        platformSpawner = GetComponent<PlatformSpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer==1200){
            timer = 0;
        }else{
            timer++;
        }
        
        Vector3 pos = new Vector3 (Player.transform.position.x+30,0,Player.transform.position.z);
        if (timer == 0)
        {
            Debug.Log("Time's up");
            if (Player.GetComponent<PlayerController>().Count >= 1)
            {
                obstacleSpawner.addBlock(pos);
                // Debug.Log("Added at " + pos);
            }
        }
    }

    public void SpawnTriggerEntered()
    {
        platformSpawner.MovePlat();
    }
}
