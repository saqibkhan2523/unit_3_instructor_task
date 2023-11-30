using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereMovement_T : MonoBehaviour
{
    //public int bounceChance = 50;
    //private int bounced = 0;

    private SpawnManager_T spawnManagerScript;

    private PlayerController_T playerControllerScript;

    private Vector3 spawnOffset = new Vector3(1.2f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager_T>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController_T>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //If gameobject is bullet increase score and generate 2 smaller balls at same position. After that Destroy this.
            Debug.Log("Divided into 2 smaller Spheres");
            Destroy(collision.gameObject);
            if(transform.localScale.x > 1)
            {
                float sphereSize = transform.localScale.x - 1;
                Vector3 sphereScale = new Vector3(sphereSize, sphereSize, sphereSize);
                spawnManagerScript.InstantiateSphere(transform.position, sphereScale);
                Vector3 spherePos = transform.position + spawnOffset;
                spawnManagerScript.InstantiateSphere(spherePos, sphereScale);
            }
            Destroy(gameObject);
            playerControllerScript.score++;
            Debug.Log("Score: " + playerControllerScript.score);
        }

        //if(collision.gameObject.CompareTag("Ground"))
        //{
        //    bounced ++;
        //    if(bounced >= bounceChance)
        //    {
        //        Destroy(gameObject);
        //        //Decrease Life.
        //    }
        //}
    }
}
