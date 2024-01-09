using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class coin : MonoBehaviour
{
    public int rotateSpeed = 5;
    Ui ui;
    public ParticleSystem coinParticle;
    ParticleSystem particle;
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<Ui>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collide with coin");
        if (other.tag == "Player")
        {
            ui.coinCollectSound();
            Destroy(this.gameObject);
            ui.scoreInc();
            particle = Instantiate(coinParticle, this.gameObject.transform.position + new Vector3(0, -1, 5), Quaternion.identity);
            particle.Play();
        }
    }
}