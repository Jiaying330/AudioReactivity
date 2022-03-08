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
        pos = new Vector3(Player.transform.position.x + 30, 0, Player.transform.position.z);

        if (Player.GetComponent<PlayerController>().Count >= 1 && spawning == false)
        {
            SpwanBox();
        }

        // if (Player.GetComponent<PlayerController>().Count >= 2)
        // {
        //     obstacleSpawner.addMiniBlock (pos);
        // }
        // if (Player.GetComponent<PlayerController>().Count >= 3)
        // {
        //     obstacleSpawner.addProjectile (pos);
        // }
        // if (Player.GetComponent<PlayerController>().Count >= 4)
        // {
        //     obstacleSpawner.addGap(platformSpawner.plats[0]);
        // }
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
        obstacleSpawner.addBlock(pos);
        spawning = false;
    }

    public void SpawnTriggerEntered()
    {
        platformSpawner.MovePlat();
    }
}
