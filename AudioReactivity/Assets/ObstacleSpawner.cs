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

    public void addMiniBlock()
    {
    }

    public void addProjectile()
    {
    }

    public void addGap()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
