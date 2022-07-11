using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    //[SerializeField] Transform checkTarget;
    [SerializeField] int checkTime = 2;
    EnemyGun[] enemyGun;

    [SerializeField] Text mugunghwa;


    Vector3 curPos;
    Vector3 lastPos;
    bool isChecking;
    float time;
    float lerpDuration = 0.5f;
    AudioManager audioManager;

    public bool gameOver = false;
    public bool playerDead = false;
    
    
    void Start()
    {
        enemyGun = FindObjectsOfType<EnemyGun>();
        audioManager = FindObjectOfType<AudioManager>();
        gameOver = false;
        StartCoroutine("RotateBack", checkTime);
        

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        curPos = target.transform.position;

        RaycastHit hit;


        // transformDirection 은 방향을 world 기준으로 변경
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10) && !gameOver)
        {

            mugunghwa.enabled = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            audioManager.Play("mugunghwa");
            isChecking = false;
            
        }
        else
        {

            mugunghwa.enabled = false;
            audioManager.StopPlaying("mugunghwa");
            isChecking = true;
        }
    }

    IEnumerator RotateBack(int waitTime)
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

        
        lastPos = target.transform.position;

        StartCoroutine ("Check");

        StartCoroutine("RotateBack", 2f);

    }
    IEnumerator Check()
    {
        float timeElapsed = 0;
        float checkDuration = 1.8f;

        while (timeElapsed < checkDuration)
        {
            
            if (curPos != lastPos && isChecking)
            {
                playerDead = true;
                foreach (EnemyGun enemy in enemyGun)
                {
                    FinishiLine finish = FindObjectOfType<FinishiLine>();
                    if (!finish.finished)
                    {
                        enemy.Shoot();
                    }
                    
                }
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        
    }

}
