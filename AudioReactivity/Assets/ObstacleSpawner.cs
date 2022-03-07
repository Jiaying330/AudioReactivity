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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addBlock(Vector3 position)
    {
        int theX = blockCount+10;
        Instantiate(block,
        position + new Vector3(theX, 1, 0),
        Quaternion.identity);
        blockCount++;
    }

    public void addMiniBlock(Vector3 position)
    {
        int theX = blockCount+8;
        Instantiate(miniBlock,
        position + new Vector3(theX, 0.5f, 0),
        Quaternion.identity);
        blockCount++;
    }

    public void addProjectile(Vector3 position)
    {
        Instantiate(projectile,
        position + new Vector3(0, 2.5f, 0),
        Quaternion.identity);
    }

    public void addGap(GameObject plat)
    {
        plat.transform.localScale.Set(29,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
