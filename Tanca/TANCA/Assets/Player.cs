
[System.Serializable]
public class Player
{
    public string playerName;
    public float mmr;
    public int gamesPlayed;

    public Player(string _playerName , float _mmr, int _gamesPlayed)
    {
        playerName = _playerName;
        mmr = _mmr;
        gamesPlayed = _gamesPlayed;

    }
}
