using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text scoreBoard;
    void Start()
    {
        StartCoroutine(UpdateScoreRoutine());
    }

    private IEnumerator UpdateScoreRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(2f);

        int scoreTeamA = 0;
        int scoreTeamB = 0;

        while (true)
        {          
            string formattedText = $"{scoreTeamA:D2}  {scoreTeamB:D2}";
            scoreBoard.text = formattedText;

            if (Random.value > 0.5f)
            {
                scoreTeamA++;
            }
            else
            {
                scoreTeamB++;
            }

            yield return waitTime;
        }
    }
}
