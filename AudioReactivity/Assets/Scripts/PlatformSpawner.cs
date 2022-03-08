using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> plats;
    private float offset = 30f;

    public GameObject note;
    public int platCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (plats != null && plats.Count > 0)
        {
            plats = plats.OrderBy(r => r.transform.position.x).ToList();
        }
    }

    public void MovePlat()
    {

        GameObject movePlat = plats[0];
        plats.Remove(movePlat);
        float newX = plats[plats.Count - 1].transform.position.x + offset;
        movePlat.transform.position = new Vector3(newX, 0, 0);
        plats.Add(movePlat);

        platCount++;
        if (platCount % 2 == 0)
        {
            //generate Notes
            Instantiate(note, new Vector3(newX, 2.5f, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
