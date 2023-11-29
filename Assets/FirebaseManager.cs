using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using SimpleJSON;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
    public const string url = "https://ratopiadata-default-rtdb.asia-southeast1.firebasedatabase.app/";

    public const string secret = "06jbWCcOcVQZo16HkfPkBNV2s2ZSKhpbQtuKIpWV";
    

    [System.Serializable]
    public class SavingCardCollectionData
    {
        public List<Player> PlayersData = new List<Player>();
    }
    
    [System.Serializable]
    public class SavingFeedbackData
    {
        public List<FeedbackData> FeedbackDatas = new List<FeedbackData>();

        public SavingFeedbackData(FeedbackData feed)
        {
            FeedbackDatas.Add(feed);
        }
    }

    public SavingCardCollectionData savingCardCollectionData;
    public SavingFeedbackData savingFeedbackData;

    public void CalRankScore()
    {
        GetPlayerData();
        
        List<Player> sortingPlayer = new List<Player>();

        sortingPlayer = savingCardCollectionData.PlayersData.OrderByDescending(player => player.PlayerScore).ToList();
        savingCardCollectionData.PlayersData = sortingPlayer;
        
        PushPlayerData();
    }
    

    private void Start()
    {
        GetPlayerData();
        PushPlayerData();
        CalRankScore();
    }

    public void PushFeedbackData(FeedbackData newFeedback)
    {
        //savingFeedbackData.FeedbackDatas.Add(newFeedback);
        string urlData = $"{url}/SavingFeedbackData.json?auth={secret}";
        // Check if existingData is null or create a new instance if it doesn't exist yet

        RestClient.Post<SavingFeedbackData>(urlData, newFeedback).Then(response =>
        {
          //  Debug.Log("Data added successfully to Firebase");
            //savingFeedbackData.FeedbackDatas.Clear();
        }).Catch(putError =>
        {
          //  Debug.Log("Error while adding data to Firebase: " + putError);
        });
    }

    public void PushPlayerData()
    {
        string urlData = $"{url}/PlayerData.json?auth={secret}";
        RestClient.Put<SavingCardCollectionData>(urlData, savingCardCollectionData).Then(testData =>
        {
            //Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
           // Debug.Log("error on server?");
        });
    }

    private int testnum = 0;
    public void GetPlayerData()
    {
        string urlData = $"{url}/PlayerData.json?auth={secret}";

        RestClient.Get(urlData).Then(response =>
        {
            

        }).Catch(error =>
        {
         //   Debug.Log("Error can't get data");
        });
    }
}
