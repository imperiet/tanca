using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ScoreBoardPositioner : Singleton<ScoreBoardPositioner>
{
    public List<GraphicPlayer> graphicPlayers;
    public GameObject scoreBoardElementPrefab;
    public RectTransform parent;

    public void Start()
    {
        List<Player> allPlayers = DataBaseAccessor.Instance.GetAllPlayers();

        foreach (Player player in allPlayers)
        {
            AddNewPlayer(player);

        }
        UpdateRating();
    }

    void UpdateAllPlayerGraphics()
    {

    }

    public void AddNewPlayer(Player newPlayer)
    {
        if (newPlayer != null)
        {
            GraphicPlayer gp = Instantiate(scoreBoardElementPrefab, parent).GetComponent<GraphicPlayer>();
            //gp.transform.localScale = Vector3.one;
            //gp.rect.anchoredPosition = Vector2.zero;
            //gp.rect.rect.Set(0,0, parent.rect.width,120);
            gp.playerData = newPlayer;
            gp.SetPlayerValues(newPlayer);
            graphicPlayers.Add(gp);
        }
    }

    public void UpdateRating()
    {
        List<Player> allPlayers = DataBaseAccessor.Instance.GetAllPlayers();

        for (int i = 0; i < graphicPlayers.Count; i++)
        {
            graphicPlayers[i].SetPlayerValues(allPlayers[i]);
        }

        GraphicPlayer[] graphicPlayersArray = graphicPlayers.ToArray();

        Array.Sort(graphicPlayersArray, delegate (GraphicPlayer x, GraphicPlayer y) { return x.playerData.mmr.CompareTo(y.playerData.mmr); });

        //graphicPlayersArray.Reverse();

        graphicPlayers = graphicPlayersArray.OfType<GraphicPlayer>().ToList();

        for (int i = 0; i < graphicPlayers.Count; i++)
        {
            graphicPlayers[i].rank = graphicPlayers.Count - i - 1;
            graphicPlayers[i].rect.anchoredPosition = Vector2.down * (graphicPlayers[i].rank * 80+50);
        }

        //parent.rect.size.Set(parent.rect.width,graphicPlayers.Count*80+100);

        parent.sizeDelta = new Vector2(parent.sizeDelta.x, graphicPlayers.Count * 80 + 100);
    }
}
