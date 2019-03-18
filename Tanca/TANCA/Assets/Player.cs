using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{ 
    public string playerName;
    public float mmr = 1000;

    public Player(string _playerName)
    {
        playerName = _playerName;
    }
}
