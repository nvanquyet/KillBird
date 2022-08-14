using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs 
{
    private static string BEST = "best_score";

    public static int get_bestScores()
    {   
        return PlayerPrefs.GetInt(BEST, 0);
    }

    public static void set_bestScores(int value)
    {
        int currentScore = PlayerPrefs.GetInt(BEST);
        if(value > currentScore)
        {
            PlayerPrefs.SetInt(BEST, value);
        }
    }

}
