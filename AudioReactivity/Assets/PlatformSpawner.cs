using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> plats;
    private float offset = 30f;
    // Start is called before the first frame update
    void Start()
    {
        if(plats != null && plats.Count >0){
            plats = plats.OrderBy(r=> r.transform.position.x).ToList();
        }
    }

    public void MovePlat()
    {
        GameObject movePlat = plats[0];
        plats.Remove(movePlat);
        float newX = plats[plats.Count - 1].transform.position.x + offset;
        movePlat.transform.position =new Vector3(newX,0,0);
        plats.Add(movePlat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
