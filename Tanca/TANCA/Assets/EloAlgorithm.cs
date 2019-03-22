using UnityEngine;

public class EloAlgorithm : Singleton<EloAlgorithm>
{
    [SerializeField] int pointsNeededForAWin = 7;

    [SerializeField] float minimumScoreForAWin = 0.25f;



    public void UpdateElo(Player player1, Player player2, int pointsPlayer1, int pointsPlayer2)
    {
        float player1Score, player2Score, player1EScore, player2EScore;



        Player p1 = DataBaseAccessor.Instance.GetPlayerData(player1.userName);
        Player p2 = DataBaseAccessor.Instance.GetPlayerData(player2.userName);

        //calaculates player1 and player2's scores from the points in the game to a value between 1 and 0
        if (pointsPlayer1 > pointsPlayer2)
        {
            player1Score = 1;
            player2Score = 0;
        }
        else if (pointsPlayer2 > pointsPlayer1)
        {
            player1Score = 0;
            player2Score = 1;
        }
        else
        {
            player1Score = 0.5f;
            player2Score = 0.5f;
        }

        //calculates the expected score(on a scale from 0 to 1) of the game depending on the players mmr.
        player1EScore = 1 / (1 + Mathf.Pow(10, (p2.mmr - p1.mmr) / 400));
        player2EScore = 1 - player1EScore;

        int winnerPoints = Mathf.Abs( pointsPlayer1 - pointsPlayer2);

        float multiplier = minimumScoreForAWin * (Mathf.Pow(1f/minimumScoreForAWin,(float)winnerPoints/(float)pointsNeededForAWin));
        Debug.Log("Multiplier: "+multiplier);

        //updates the players mmr depending on how well they played compared to their mmr
        float p1mmr = player1.mmr += multiplier * 64f * (player1Score - player1EScore);
        float p2mmr = player2.mmr += multiplier * 64f * (player2Score - player2EScore);

        player1.gamesPlayed++;
        player2.gamesPlayed++;

        Player tempPlayer1 = new Player(player1.userName, p1mmr, player1.gamesPlayed, 0);
        Player tempPlayer2 = new Player(player2.userName, p2mmr, player2.gamesPlayed, 0);

        Debug.Log(p1mmr + "__" + p2mmr);

        DataBaseAccessor.Instance.UpdatePlayerData(tempPlayer1);
        DataBaseAccessor.Instance.UpdatePlayerData(tempPlayer2);
    }
}