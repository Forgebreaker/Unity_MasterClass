using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Yellow_Score;
    [SerializeField] private TMP_Text Blue_Score;

    private int Yellow_Player1 = 0;
    private int Blue_Player2 = 0;

    private void OnEnable()
    {
        PlayerLogic.OnPlayerDeath += UpdateScore;
    }
    private void OnDisable()
    {
        PlayerLogic.OnPlayerDeath -= UpdateScore;
    }

    void UpdateScore(int Player_ID)
    {
        if (Player_ID == 2) 
        {
            Yellow_Player1 += 1;
            Yellow_Score.text = "Yellow: " + Yellow_Player1;
        }

        if (Player_ID == 1)
        {
            Blue_Player2 += 1;
            Blue_Score.text = "Blue: " + Blue_Player2;
        }
    }
}
