using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
   public string userName;
   public float mmr;
   public int gamesPlayed;
   public float winRatio;

    public Player(string _userName, float _mmr, int _gamesPlayed , float _winRatio)
    {
        userName = _userName;
        mmr = _mmr;
        gamesPlayed = _gamesPlayed;
        winRatio = _winRatio;
    }

    public Player(string _userName)
    {
        userName = _userName;
        mmr = 1000;
        gamesPlayed = 0;
        winRatio = 0;
    }
}
