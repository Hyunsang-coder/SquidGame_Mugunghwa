using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishiLine : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager manager;
    EnemyGun enemyGun;
    Enemy enemy;
    public bool finished;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        enemyGun = FindObjectOfType<EnemyGun>();
        enemy = FindObjectOfType<Enemy>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !enemy.playerDead)
        {
            finished = true;
            enemyGun.gameOver = true;
            manager.MissionComplete();
        }
    }
}
