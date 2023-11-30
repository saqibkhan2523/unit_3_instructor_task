using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MoveUp_T : MonoBehaviour
{
    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * projectileSpeed);

        if(transform.position.y > 15f)
        {
            Destroy(gameObject);
        }
    }
}
