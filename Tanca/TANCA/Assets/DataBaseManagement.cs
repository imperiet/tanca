using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;



public class DataBaseManagement : Singleton<DataBaseManagement>
{
    [SerializeField] private TextAsset file;
    private string path;

    private void LoadFile()
    {

    }
    
    public void NewPlayer(string userName)
    {
        path = AssetDatabase.GetAssetPath(file);

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(userName + ",1000,0");

        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);


        //Print the text from the file
        Debug.Log(file.text);
    }

    public Player tempPlayer;

    [ContextMenu("petera")]
    public void TestPlayer(){
        UpdatePlayerInfo(tempPlayer);
    }

    public void UpdatePlayerInfo(Player player)
    {
        path = AssetDatabase.GetAssetPath(file);

        StreamReader reader = new StreamReader(path);

        string playerString = "";

        int lineNumber = -1;

        while (!playerString.Contains(player.playerName + ",") && playerString != null)
        {
            playerString = reader.ReadLine();
            lineNumber++;
        }

        Debug.Log(playerString);
        reader.Close();




        string[] lines = System.IO.File.ReadAllLines(path);
        lines[lineNumber] = player.playerName + "," + player.mmr.ToString().Replace(",",".") + "," + player.gamesPlayed;

        StreamWriter sw = new StreamWriter(path, false);

        foreach (var str in lines)
        {
            Debug.Log(str);
            sw.WriteLine(str);
        }

        sw.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
    }

    public Player GetPlayerInfo(string userName)
    {
        path = AssetDatabase.GetAssetPath(file);

        StreamReader reader = new StreamReader(path);

        string playerString = "";

        while (!playerString.Contains(userName + ",") && playerString != null)
        {
            playerString = reader.ReadLine();
        }

        Debug.Log(playerString);
        reader.Close();

        string[] playerData = playerString.Split(',');

        foreach (var str in playerData)
        {
            Debug.Log(str);
        }

        return new Player (playerData[0],float.Parse(playerData[1].Replace(".",",")),int.Parse(playerData[2]));
    }
}
