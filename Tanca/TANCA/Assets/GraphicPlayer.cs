using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicPlayer : MonoBehaviour
{
    public int rank;
    public RectTransform rect;
    public TMP_Text _playerName, _mmr, _gamesPlayed;
    public Player playerData;

    

    public void SetPlayerValues(Player player)
    {
        playerData = player;
        _playerName.text = player.userName;
        _mmr.text = ""+player.mmr;
        _gamesPlayed.text = ""+player.gamesPlayed;
    }
}
