using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject block;

    public GameObject miniBlock;

    public GameObject projectile;

    public int blockCount;
    public int miniBlockCount;
    public int projectileCount;

    public void addBlock(Vector3 position)
    {
        int theX = blockCount + 9;
        Instantiate(block,
        position + new Vector3(theX, 1, 0),
        Quaternion.identity);
        // blockCount++;
    }

    public void addMiniBlock(Vector3 position)
    {
        int theX = blockCount + 10;
        Instantiate(miniBlock,
        position + new Vector3(theX, 0.75f, 0),
        Quaternion.identity);
        // blockCount++;
    }

    public void addProjectile(Vector3 position)
    {
        Instantiate(projectile,
        position + new Vector3(3, 2.5f, 0),
        Quaternion.identity);
    }

    public void addGap(GameObject plat)
    {
        plat.transform.localScale = new Vector3(28, 1, 1);
        // plat.transform.localScale-= new Vector3(1,0,0);
        Debug.Log("gap");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
