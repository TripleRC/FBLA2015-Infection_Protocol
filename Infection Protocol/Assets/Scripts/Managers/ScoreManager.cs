using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * This script displays the score
 * It's public score variable allows other scripts to change it
 * */
public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }
}
