using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction = destination - source
        Vector3 directionToFace = target.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToFace);
        Debug.DrawRay(transform.position, directionToFace, Color.red);


    }

    public void Shoot()
    {
        var enemyFire = GetComponent<ParticleSystem>().emission;
        enemyFire.enabled = true;
    }
}
