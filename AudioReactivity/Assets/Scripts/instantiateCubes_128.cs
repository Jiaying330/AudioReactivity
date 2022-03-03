using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateCubes_128 : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    public float maxScale;
    GameObject[] sampleCube = new GameObject[256];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 256; i++) {
            GameObject instanceSampleCube = (GameObject)Instantiate (sampleCubePrefab);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "SampleCube_128" + i;
            // this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
            this.transform.eulerAngles = new Vector3 (0, -1.40625f * i, 0);
            instanceSampleCube.transform.position = Vector3.forward * 90;
            sampleCube[i] = instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 256; i++) {
            if (sampleCube != null) {
                sampleCube[i].transform.localScale = new Vector3(1, (AudioPeer.samples[i] * maxScale) + 2, 1);
            }
        }
    }
}
