using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Hurdles : MonoBehaviour
{
    public GameObject[] hurdles;
    int randomHurdle;
    float hurdlePositionZ = 10;
    bool loop = true;
    float[] coneHorizontalLoc = { -2.5f, 0, 2.5f };
    public GameObject parent;
    GameObject currentHurlde;
    public GameObject coin;
    GameObject currentCoin;
    public int distanceBetweenHurdles;
    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Debug.Log(distanceBetweenHurdles);
        while (loop)
        {
            if (transform.position.z + 225 < transform.position.z + hurdlePositionZ)
            {
                loop = false;
                break;
            }
            randomHurdle = Random.Range(0, 3);
            switch (randomHurdle)
            {
                case 0:
                    {
                        hurdles[randomHurdle].transform.position = new Vector3(coneHorizontalLoc[Random.Range(0, 3)], 0.25f, 0);
                    }
                    break;
                case 1:
                    {
                        float randomValue = Random.Range(0, 2) == 0 ? 1.33f : -1.33f;
                        hurdles[randomHurdle].transform.position = new Vector3(randomValue, 0.834f, 0);
                    }
                    break;
                case 2:
                    {
                        float randomValue = Random.Range(0, 2) == 0 ? 1.35f : -1.35f;
                        hurdles[randomHurdle].transform.position = new Vector3(randomValue, 2.2f, 0);
                    }
                    break;
            }
            currentHurlde = Instantiate(hurdles[randomHurdle], hurdles[randomHurdle].transform.position + new Vector3(0, 0, transform.position.z + hurdlePositionZ), hurdles[randomHurdle].transform.rotation);
            currentHurlde.transform.parent = parent.transform;
            hurdlePositionZ += distanceBetweenHurdles;
            Vector3 coinRandomPos = new Vector3(coneHorizontalLoc[Random.Range(0, 3)], 0.87f, 0);
            for (int i = 0; i < 5; i++)
            {
                currentCoin = Instantiate(coin, coinRandomPos + new Vector3(0, 0, transform.position.z + hurdlePositionZ + 4 + i), Quaternion.identity);
                currentCoin.transform.parent = parent.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void increaseHurdlesDistance()
    {
        distanceBetweenHurdles++;
    }
    public void setHurdleDistance()
    {
        distanceBetweenHurdles = 15;
    }
}
