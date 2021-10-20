using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager bgm = FindObjectOfType<AudioManager>();
        bgm.Play("funnySong");
    }

    // Update is called once per frame
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        
    }
}
