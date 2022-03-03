using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject[] planets = new GameObject[6];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        int numPartitions = 6;
		float[] aveMag = new float[numPartitions];
		float partitionIndx = 0;
		int numDisplayedBins = 256 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

		for (int i = 0; i < numDisplayedBins; i++) 
		{
			if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
				aveMag[(int)partitionIndx] += AudioPeer.samples_1[i] / (256/numPartitions);
			}
			else
            {
				partitionIndx++;
				i--;
			}
		}

        //scale and bound the average magnitude.
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = aveMag[i] * 100;
            if (aveMag[i] > 100)
            {
                aveMag[i] = 100;
            }
        }
        for (int i = 0; i < 6; i++) {
            if(this.gameObject.name == planets[i].name) {
               transform.localScale = new Vector3 (aveMag[i]*10, aveMag[i]*10, aveMag[i]*10);
            //    Debug.Log(aveMag[i]);   
               break;
            }
             
        } 
		}
    
        
}
