using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int Score;
    public int giftscore;
    public Text scoretext, gifttext, resultscoretext, resultgifttext;

    public GameObject gameover, home, pausemenu, gameplay;

    public bool isgamepaused;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        gameover.SetActive(false);
        home.SetActive(true);
        pausemenu.SetActive(false);
        gameplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = Score.ToString();
        gifttext.text = giftscore.ToString();
    }

    public void startgame()
    {
        home.SetActive(false);
        gameplay.SetActive(true);

        Time.timeScale = 1;
    }

    public void ShowGameOver()
    {
        gameover.SetActive(true);
        resultscoretext.text = scoretext.text;
        resultgifttext.text = gifttext.text;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause(bool pause)
    {
        isgamepaused = pause;

        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        gameplay.SetActive(!pause);
        pausemenu.SetActive(pause);
    }
}
