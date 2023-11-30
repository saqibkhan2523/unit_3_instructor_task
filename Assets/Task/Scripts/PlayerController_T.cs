using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_T : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float horizontalInput;

    public float playerSpeed = 10f;

    private Animator playerAnim;

    public Vector3 bulletPosOffset;

    public bool isPlayerMoving = false;

    public int lives = 3;

    public int score = 0;

    public bool gameOver = false;

    public ParticleSystem explosionPrefab;
    public ParticleSystem fireworkPrefab;

    float timeStopTimer = 4f;
    public bool stopGame = false;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            playerAnim.SetFloat("Speed_f", 1);
            isPlayerMoving = true;
        }
        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            playerAnim.SetFloat("Speed_f", 1);
            isPlayerMoving = true;
        }
        if (horizontalInput == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            playerAnim.SetFloat("Speed_f", 0.2f);
            isPlayerMoving = false;
        }

        
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * playerSpeed, Space.World);

        if (transform.position.x >= 19)
        {
            transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -19)
        {
            transform.position = new Vector3(-20, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isPlayerMoving && !gameOver)
        {
            //Shoot bullet and play animation.
            playerAnim.SetTrigger("Jump_trig");
            Instantiate(bulletPrefab, transform.position + bulletPosOffset, Quaternion.identity);
        }

        if(stopGame)
        {
            timer += Time.deltaTime;
            if(timer > timeStopTimer) 
            {
                timer = 0;
                stopGame = false;
                ResumeAllSpheres();
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            lives--;
            Debug.Log("Lives: " + lives);
            explosionPrefab.Play();
            if (lives == 0)
            {
                Debug.Log("Game Over");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 2);
                gameOver = true;
                
            }
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("Life"))
        {
            lives++;
            Debug.Log("Lives: " + lives);
            Destroy(collision.gameObject);
            fireworkPrefab.Play();
        }

        //LOGIC NOT WORKING.
        //if (collision.gameObject.CompareTag("TimeStop"))
        //{
        //    Debug.Log("Time Stop");
        //    stopGame = true;
        //    StopAllSpheres();
        //    Destroy(collision.gameObject);
        //    fireworkPrefab.Play();
        //}


    }

    private void StopAllSpheres()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }

    private void ResumeAllSpheres()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}
