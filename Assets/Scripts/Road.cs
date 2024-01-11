using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject player;
    float spwanRoad = -10;
    int roadSpwanLoc = 60;
    public GameObject road;
    float prevRoad = 130;
    List<GameObject> raodList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > spwanRoad)
        {

            raodList.Add(Instantiate(road, new Vector3(0, 0, roadSpwanLoc), Quaternion.identity));
            roadSpwanLoc += 60;
            spwanRoad += 60;
        }
        if (player.transform.position.z > prevRoad)
        {
            Destroy(raodList[0]);
            raodList.RemoveAt(0);
            prevRoad += 60;
        }
    }
}
