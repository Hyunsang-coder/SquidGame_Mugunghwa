using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    [SerializeField] float lerpDuration = 0.5f;
    bool isRotate;
    float time = 0;
    int startTime = 2;

    [SerializeField] GameObject target;
    EnemyGun enemyGun;


    void Start()
    {
        isRotate = true;
        enemyGun = FindObjectOfType<EnemyGun>();
        target.transform.hasChanged = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            time += Time.deltaTime;
            int intTime = Mathf.RoundToInt(time);
            Debug.Log(intTime);

            if (intTime > startTime)
            {
                isRotate = false;
                StartCoroutine("Rotate180", 1f);
            }

        }
    }

    IEnumerator Rotate180(int waitTime)
    {
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);

        yield return new WaitForSeconds(waitTime);
        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = targetRotation;
        Invoke ("CheckMove", 0.5f);
        

    }

    void CheckMove()
    {
        if (target.transform.hasChanged)
        {
            
            enemyGun.Shoot();
            print("You are moving!");
            return;

        }

        else
        {
            StartCoroutine("Rotate180", 1f);
        }

    }
}
