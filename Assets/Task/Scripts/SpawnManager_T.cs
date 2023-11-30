using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_T : MonoBehaviour
{
    public GameObject spherePrefab;

    public GameObject[] lifeAndTimeStopPrefab;

    private PlayerController_T playerControllerScript;

    float sphereSize_f = 1f;
    float spherePosRange_X = 20f;
    float spherePos_Y = 20f;

    float startDelay = 2f;
    float repeatInterval = 4f;

    float powerUpStartDelay = 3f;
    float powerUpRepeatInterval = 6f;
    float powerUpSpherePosRange_X = 18f;

    float waveTime = 10f;
    float timer = 0f;

    //For wave system, create a timer, and whenever timer is complete increase the minimize the repeatInterval and reset the timer. (Bonus)

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController_T>();

        InvokeRepeating("SpawnSphere", startDelay, repeatInterval);
        InvokeRepeating("SpawnLifeAndTimeStop", powerUpStartDelay, powerUpRepeatInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver)
        {
            CancelInvoke("SpawnSphere");
            CancelInvoke("SpawnLifeAndTimeStop");

            GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

            for (int i = 0; i < spheres.Length; i++)
            {
                Destroy(spheres[i]);
            }
        }

        timer += Time.deltaTime;
        if(timer > waveTime)
        {
            timer = 0f;
            repeatInterval -= 0.5f;
            CancelInvoke("SpawnSphere");
            if (repeatInterval > 0f)
            {
                InvokeRepeating("SpawnSphere", startDelay, repeatInterval);
            }
        }

    }

    private void SpawnSphere()
    {
        if (!playerControllerScript.stopGame)
        {
            sphereSize_f = (float)Random.Range(1, 4);
            Vector3 sphereScale = new Vector3(sphereSize_f, sphereSize_f, sphereSize_f);

            Vector3 spherePos = new Vector3(Random.Range(-spherePosRange_X, spherePosRange_X), spherePos_Y, 0);

            InstantiateSphere(spherePos, sphereScale);
        }

    }

    public void InstantiateSphere(Vector3 spherePos, Vector3 sphereScale)
    {
        GameObject sphere = Instantiate(spherePrefab, spherePos, Quaternion.identity);

        sphere.transform.localScale = sphereScale;
    }

    private void SpawnLifeAndTimeStop()
    {
        if (!playerControllerScript.stopGame)
        {
            int arrayIndex = Random.Range(0, 2);
            Vector3 spherePos = new Vector3(Random.Range(-powerUpSpherePosRange_X, powerUpSpherePosRange_X), spherePos_Y, 0);

            //Instantiate(lifeAndTimeStopPrefab[arrayIndex], spherePos, Quaternion.identity);
            Instantiate(lifeAndTimeStopPrefab[1], spherePos, Quaternion.identity);
        }
    }


}
