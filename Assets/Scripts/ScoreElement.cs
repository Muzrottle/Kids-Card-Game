using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElement : MonoBehaviour
{
    public Text userText;
    public Text scoreText;

    public void NewScoreElement(string _username, int _score)
    {
        userText.text = _username;
        scoreText.text = _score.ToString();
    }

}
