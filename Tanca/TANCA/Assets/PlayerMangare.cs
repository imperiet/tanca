using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMangare : Singleton<PlayerMangare>
{
    public List<Player> players = new List<Player>();

    public TMPro.TMP_InputField inputField;
    public Button addButton;

    private void Awake()
    {
        addButton.onClick.AddListener(delegate { AttemptAdd(); });
    }

    private void AttemptAdd()
    {
        print("adding");
        if (inputField.text != "")
        {
            AddNewPlayer(inputField.text);
        }
    }

    public void AddNewPlayer(string _playerName)
    {
        if (players.Count > 0)
        {
            foreach (Player player in players)
            {
                if (player.playerName == _playerName)
                {
                    Debug.LogError("That name is taken :(");
                    return;
                }
            }
        }

        players.Add(new Player(_playerName));
    }

    public void UpdatePlayerCount()
    {

    }
}
