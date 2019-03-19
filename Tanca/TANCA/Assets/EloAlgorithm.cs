using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EloAlgorithm : Singleton<EloAlgorithm>
{
    void GetNewElo(Player player1, Player player2, int pointsPlayer1, int pointsPlayer2)
    {
        float player1Score, player2Score, player1EScore, player2EScore;

        int pointsNeededForAWin = 7;

        float minimumScoreForAWin = 0.75f;

        Player p1 = DataBaseManagement.Instance.GetPlayerInfo(player1.playerName);
        Player p2 = DataBaseManagement.Instance.GetPlayerInfo(player2.playerName);

        //calaculates player1 and player2's scores from the points in the game to a value between 1 and 0
        if (pointsPlayer1 - pointsPlayer2 > 0)
        {
            player1Score = minimumScoreForAWin + (pointsPlayer1 - pointsPlayer2 - 1f) * (1 - minimumScoreForAWin) / (pointsNeededForAWin - 1);
            player2Score = 1 - player1Score;
        }

        else if (pointsPlayer2 - pointsPlayer1 > 0)
        {
            player2Score = minimumScoreForAWin + (pointsPlayer2 - pointsPlayer1 - 1f) * (1 - minimumScoreForAWin) / (pointsNeededForAWin - 1);
            player1Score = 1 - player2Score;
        }

        else
        {
            player1Score = 0.5f;
            player2Score = 0.5f;
        }

        //calculates the expected score(on a scale from 0 to 1) of the game depending on the players mmr.
        player1EScore = 1 / (1 + Mathf.Pow(10, (p2.mmr - p1.mmr) / 400));
        player2EScore = 1 - player1EScore;

        //updates the players mmr depending on how well they played compared to their mmr
        float p1mmr = player1.mmr += 64f * (player1Score - player1EScore);
        float p2mmr = player2.mmr += 64f * (player2Score - player2EScore);


        Player tempPlayer1 = new Player(player1.playerName, p1mmr, player1.gamesPlayed++);
        Player tempPlayer2 = new Player(player2.playerName, p2mmr, player2.gamesPlayed++);

        DataBaseManagement.Instance.UpdatePlayerInfo(tempPlayer1);
        DataBaseManagement.Instance.UpdatePlayerInfo(tempPlayer2);
    }
}
