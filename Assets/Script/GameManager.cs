using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject playScreen;
    public GameObject gameOverScreen;
    public GameObject gameFinishScreen;

    [SerializeField] Text playtimeTxt;
    [SerializeField] Text gameRecordTxt;


    float playTime = 0;
    bool isPlaying;
    void Start()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            playTime += Time.deltaTime;
        }
        ReStart();
        Quit();
       
        
    }

    void LateUpdate()
    {
        playtimeTxt.text = "Time: " + playTime.ToString("N2");
        gameRecordTxt.text = "Your Record: " + playTime.ToString("N2");

    }

    public void StopTime()
    {
        isPlaying = false;
    }

    void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void ReStart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReLoadScene();
        }
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MissionComplete()
    {
        

        StopTime();
        playScreen.SetActive(false);
        gameFinishScreen.SetActive(true);
        var audio = FindObjectOfType<AudioManager>();
        audio.StopPlaying("funnySong");
        
        audio.Play("missionComplete");

        Enemy enemy = FindObjectOfType<Enemy>();
        enemy.StopCoroutine("RotateBack");
        enemy.gameOver = true;

        
        

    }

    public void GameOver()
    {
        StopTime();
        playScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        var audio = FindObjectOfType<AudioManager>();
        audio.StopPlaying("funnySong");

        Enemy enemy = FindObjectOfType<Enemy>();
        enemy.StopCoroutine("RotateBack");
    }

}
