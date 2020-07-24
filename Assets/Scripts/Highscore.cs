using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public Text highscore;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("highscore", 0);
        highscore.text = "High Score: " + score.ToString();
    }
}
