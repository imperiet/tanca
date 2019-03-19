using UnityEngine;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField player1field, player2field;
    [SerializeField] Slider s1, s2;
    [SerializeField] Player player1, player2;

    public void EnterResults()
    {
        foreach (Player player in PlayerMangare.Instance.players)
        {
            if (player.playerName == player1field.text)
            {
                player1 = player;
                Debug.Log(player1.playerName + "vs" + player2.playerName);
            }
            else if (player.playerName == player2field.text)
            {
                player2 = player;
                Debug.Log(player1.playerName + " vs " + player2.playerName);
                
            }
            else { Debug.Log("One or more of these players do not exist"); return; }

            PerformCalculations(player1,player2);
        }
    }

    private void PerformCalculations(Player p1, Player p2)
    {
        EloAlgorithm.Instance.GetNewElo(p1,p2, (int)s1.value,(int)s2.value);
    }
}
