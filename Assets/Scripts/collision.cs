using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BarrierHigh" || collision.collider.tag == "BarrierLow" || collision.collider.tag == "TrafficCone")
        {
            Debug.Log("collide");
            ui.GetComponent<Ui>().GameOver();
        }

    }

}
