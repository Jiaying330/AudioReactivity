using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Player;

    PlatformSpawner platformSpawner;

    ObstacleSpawner obstacleSpawner;

    private int timer;

    Vector3 pos;

    bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        platformSpawner = GetComponent<PlatformSpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(Player.transform.position.x + 15, 0, Player.transform.position.z);

        if (spawning == false)
        {
            SpwanBox();
        }
    }

    public void SpwanBox()
    {
        StartCoroutine(SpawnBoxWait());
    }

    IEnumerator SpawnBoxWait()
    {
        spawning = true;
        yield return new WaitForSeconds(1.6f);
        Debug.Log("Spawned!");
        if(Player.GetComponent<PlayerController>().Count >= 1){
            obstacleSpawner.addBlock(pos);
        }
        if (Player.GetComponent<PlayerController>().Count >= 2)
        {
            obstacleSpawner.addMiniBlock (pos);
        }
        if (Player.GetComponent<PlayerController>().Count >= 3)
        {
            obstacleSpawner.addProjectile (pos);
        }
        if (Player.GetComponent<PlayerController>().Count >= 1)
        {
            obstacleSpawner.addGap(platformSpawner.plats[2]);
        }
        spawning = false;
    }

    public void SpawnTriggerEntered()
    {
        platformSpawner.MovePlat();
    }
}
