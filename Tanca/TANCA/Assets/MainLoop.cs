using UnityEngine;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField player1field, player2field;
    [SerializeField] Slider s1, s2;
    [SerializeField] Player player1, player2;

    public void EnterResults()
    {
        player1 = DataBaseManagement.Instance.GetPlayerInfo(player1field.text.ToLower());
        player2 = DataBaseManagement.Instance.GetPlayerInfo(player2field.text.ToLower());

        PerformCalculations(player1, player2);
    }

    private void PerformCalculations(Player p1, Player p2)
    {
        EloAlgorithm.Instance.GetNewElo(p1, p2, (int)s1.value, (int)s2.value);
    }
}
