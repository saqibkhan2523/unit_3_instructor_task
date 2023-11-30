using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float destroyTime = 2f;
    private float timer = 0f;

    private bool startTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            startTimer = true;
        }

        
    }
}
