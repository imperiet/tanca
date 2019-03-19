using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EloAlgorithm : Singleton<EloAlgorithm>
{
    public void GetNewElo(Player player1, Player player2, int pointsPlayer1, int pointsPlayer2)
    {
        float player1Score = 0.5f, player2Score = 0.5f, player1EScore, player2EScore;

        //calaculates player1 and player2's scores from the points in the game to a value between 1 and 0
        if (pointsPlayer1 - pointsPlayer2 > 0)
        {
            player1Score = 0.75f + (pointsPlayer1 - pointsPlayer2 - 1f) * 0.25f / 6f;
            player2Score = 1 - player1Score;
        }

        else if (pointsPlayer2 - pointsPlayer1 > 0)
        {
            player2Score = 0.75f + (pointsPlayer2 - pointsPlayer1 - 1f) * 0.25f / 6f;
            player1Score = 1 - player2Score;
        }

        else
        {
            Debug.Log("Error with the reported score");
        }

        player1EScore = 1 / (1 + Mathf.Pow(10, (player2.mmr - player1.mmr) / 400));
        player2EScore = 1 - player1EScore;

        player1.mmr += 64f * (player1Score - player1EScore);
        player2.mmr += 64f * (player2Score - player2EScore);

        player1.gamesPlayed ++;
        player2.gamesPlayed++;
    }
}
