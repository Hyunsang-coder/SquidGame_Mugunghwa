using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isMoving;
    bool isControl;
    Animator anim;
    Vector3 moveVec;
    Rigidbody rb;
    GameManager manager;
    AudioManager audioManager;

    [SerializeField] float speed = 10f;

    private void Awake()
    {
        isControl = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    

    // Update is called once per frame
    void Update()
    {

        if (isControl) 
        {
            float vAxis = Input.GetAxis("Vertical");
            float hAxis = Input.GetAxis("Horizontal");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
            transform.LookAt(transform.position + moveVec);

            anim.SetBool("isRun", moveVec != Vector3.zero);

            if (moveVec != Vector3.zero)
            {
                audioManager.Play("footStep");
            }
        }
        
        
    }

    void OnParticleCollision(GameObject other)
    {
        isControl = false;
        moveVec = Vector3.zero;
        anim.SetBool("isRun", moveVec != Vector3.zero);

        Debug.Log("Particle hit");

        manager.StopTime();
        manager.GameOver();

        audioManager.StopPlaying("funnySong");
    }
}
