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

    public void AttemptAdd()
    {
        print("adding");
        if (inputField.text != "")
        {
            AddNewPlayer(inputField.text);
        }
    }

    public void AddNewPlayer(string _playerName)
    {
        DataBaseManagement.Instance.NewPlayer(_playerName);
    }
}
