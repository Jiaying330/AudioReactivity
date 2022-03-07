using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    public float maxScale;
    GameObject[] sampleCube = new GameObject[512];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++) {
            GameObject instanceSampleCube = (GameObject)Instantiate (sampleCubePrefab);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "SampleCube" + i;
            // this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
            this.transform.eulerAngles = new Vector3 (0, 1f * i, 0);
            instanceSampleCube.transform.position = Vector3.forward * 100 ;
            sampleCube[i] = instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++) {
            if (sampleCube != null) {
                sampleCube[i].transform.localScale = new Vector3(2, (AudioPeer.samples[i] * maxScale) + 2, 2);
            }
        }
    }
}
