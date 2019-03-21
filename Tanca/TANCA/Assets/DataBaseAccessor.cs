using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataBaseAccessor : Singleton<DataBaseAccessor>
{
    public TextAsset dataFile;
    private string dataPath;

    private void Awake()
    {
        dataPath = AssetDatabase.GetAssetPath(dataFile);
    }

    public List<Player> GetAllPlayers()
    {
        List<Player> allPlayers = new List<Player>();

        StreamReader reader = new StreamReader(dataPath);

        while (!reader.EndOfStream)
        {
            string playerDataLine = reader.ReadLine();
            string[] playerData = playerDataLine.Split(',');

            allPlayers.Add(new Player(playerData[0], float.Parse(playerData[1].ToString().Replace(".", ",")), int.Parse(playerData[2]), float.Parse(playerData[3])));
        }
        reader.Close();
        return allPlayers;
    }

    public Player GetPlayerData(string userNameToFind)
    {
        StreamReader reader = new StreamReader(dataPath);

        string foundData = "";
        string foundUserName = "";

        while (!reader.EndOfStream && foundUserName != userNameToFind)
        {
            foundData = reader.ReadLine();

            foundUserName = foundData.Split(',')[0];

            Debug.Log(userNameToFind + ":" + foundData);
        }

        if (foundUserName != userNameToFind)
        {
            return null;
        }

        reader.Close();

        string[] playerData = foundData.Split(',');

        return new Player(playerData[0], float.Parse(playerData[1].ToString().Replace(".", ",")), int.Parse(playerData[2]), float.Parse(playerData[3]));
    }

    public void UpdatePlayerData(Player player)
    {
        string[] wholeDocument = File.ReadAllLines(dataPath);

        int playerIndex = 0;
        bool run = true;

        while (run && playerIndex < wholeDocument.Length)
        {
            Debug.Log("wholeDocument[playerIndex]: " + wholeDocument[playerIndex]);
            if (wholeDocument[playerIndex].Contains(player.userName + ","))
            {
                run = false;
            }
            playerIndex++;
        }

        wholeDocument[playerIndex-1] = player.userName + "," + player.mmr.ToString().Replace(",",".") + "," + player.gamesPlayed + "," + player.winRatio;

        StreamWriter writer = new StreamWriter(dataPath, false);

        foreach (string str in wholeDocument)
        {
            Debug.Log(str);
            writer.WriteLine(str);
        }

        writer.Close();
        AssetDatabase.Refresh();
    }

    public Player AddNewPlayer(string userName)
    {
        StreamReader sr = new StreamReader(dataPath);

        while (!sr.EndOfStream)
        {
            if (sr.ReadLine().Contains(userName+","))
            {
                Debug.Log("That username already exist");
                sr.Close();
                return null;
            }
        }

        sr.Close();


        StreamWriter sw = new StreamWriter(dataPath, true);

        sw.WriteLine(userName + ",1000,0,0");
        sw.Close();



        AssetDatabase.Refresh();

        return new Player(userName,1000,0,0);
    }
}
