using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    
    [SerializeField] Transform target;
    Vector3 offset;
    public bool gameOver;
    public bool playerDead;


    private void Start()
    {
        gameOver = false;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //direction = destination - source
        Vector3 directionToFace = target.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToFace);
        Debug.DrawRay(transform.position, directionToFace, Color.red);


        // Ä«¸Þ¶óÃ³·³ ÃÑµµ Å¸°ÙÀ» µû¶ó ´Ù´Ô
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.1f);

    }

    public void Shoot()
    {
        if (!gameOver)
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            var em = ps.emission;
            em.enabled = true;
            var audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("enemyShoot");

            Invoke("EnemyDestoryed", 1f);

            GameManager manager = FindObjectOfType<GameManager>();
            manager.GameOver();
        }
        

    }

    public void EnemyDestoryed() 
    {
        Destroy(gameObject);
        var audioManager = FindObjectOfType<AudioManager>();
        audioManager.StopPlaying("enemyShoot");
    }



}
