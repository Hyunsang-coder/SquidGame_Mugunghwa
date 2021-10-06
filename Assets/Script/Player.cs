using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isMoving;
    Rigidbody rb;
    [SerializeField] float speed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float forwardMove = Input.GetAxis("Vertical") * speed;
        float sideMove = Input.GetAxis("Horizontal") * speed;

        forwardMove *= Time.deltaTime;
        sideMove *= Time.deltaTime;
        transform.Translate(sideMove, 0, forwardMove);
    }
}
