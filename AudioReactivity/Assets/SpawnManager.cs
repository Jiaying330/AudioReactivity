using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PlatformSpawner platformSpawner;

    // Start is called before the first frame update
    void Start()
    {
        platformSpawner = GetComponent<PlatformSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnTriggerEntered()
    {
        platformSpawner.MovePlat();
    }
}
