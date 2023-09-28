using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{

    private static List<Player> PlayersData = new List<Player>();

    [SerializeField] private Transform leaderboardParent;

    [SerializeField] private GameObject userInfoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayersData = FindObjectOfType<FirebaseRankingManager>().ranking.PlayersData;
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
        foreach (var VARIABLE in PlayersData)
        {
            Instantiate(userInfoPrefab,leaderboardParent).GetComponent<UserPrefab>().Create(VARIABLE,i);
            i++;
        }
    }
}
