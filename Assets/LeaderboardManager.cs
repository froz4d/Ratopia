using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private static List<Player> PlayersOnServer = new List<Player>();

    [SerializeField] private Transform leaderboardParent;

    [SerializeField] private GameObject userInfoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayersOnServer.Add(new Player("Money",10));
        PlayersOnServer.Add(new Player("Yolo",100));
        PlayersOnServer.Add(new Player("Choen",7));
        PlayersOnServer.Add(new Player("Run",45));
        
        PlayersOnServer.Sort(((player, player1) => player1.PlayerScore.CompareTo(player.PlayerScore)));
        
        Debug.Log(PlayersOnServer.Count);
        
        CreateLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (this.gameObject.activeSelf)
        {
         //CreateLeaderboard();   
        }
    }

    private void CreateLeaderboard()
    {
        int i = 1;
        //remove
        foreach (Transform child in leaderboardParent)
        {
            Destroy(child.gameObject);
        }
        
        //create new
        foreach (var VARIABLE in PlayersOnServer)
        {
            Instantiate(userInfoPrefab,leaderboardParent).GetComponent<UserPrefab>().Create(VARIABLE,i);
            i++;
        }
    }
}