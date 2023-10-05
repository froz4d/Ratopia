using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using SimpleJSON;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine.Serialization;

public class FireBaseResultManager : MonoBehaviour
{
    public const string url = "https://ratopia-99-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public const string secret = "tjvNGD8hDi4AzXteV9tKjfiI1lCLrzHhE5P7I74R";

    [Header("Main")] 
    public ResultManager resultManager;
    private List<PlayerData> SortPlayerData;

    [System.Serializable]
    
    public class Ranking
    {
        public List<PlayerData> playerData = new List<PlayerData>();
        
    }

    public Ranking ranking;
    [Header("New Data")] 
    public PlayerData currantPlayerData;
    
    [Header("Test")]
    public int testnum;

    [System.Serializable]
    public class TestData
    {
        public int no = 1;
        public string name = "A";
    }

    public TestData testData;
    
    private void Start()
    {
        //DebugSetupWithLocalData();
        //TestSetData();
        //TestGetData();
        //TestSetData2();
        //TestGetData2();
        //SetLocalDataToDatabase();
        ReloadSortingData();
        
    }

    public void DebugSetupWithLocalData()
    {
        ranking.playerData = resultManager.playerData;
        CalculateRankFromWeek();
    }

    public void CalculateRankFromWeek()
    {
        SortPlayerData = ranking.playerData
            .OrderByDescending(data => data.survivalWeek).ToList();
        SortPlayerData.ForEach(data => data.noNumber = SortPlayerData.IndexOf(data)+1);
        ranking.playerData = SortPlayerData;
    }

    public void TestSetData()
    {
        string urlData = $"{url}/.json?auth={secret}";
        RestClient.Put<TestData>(urlData, testData).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log("Error On Set to Server");
        });
    }
    
    public void TestGetData()
    {
        string urlData = $"{url}/.json?auth={secret}";
        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);

            testnum = jsonNode["no"];
        }).Catch(error =>
        {
            Debug.Log("Error to Get Data");
        });
    }
    
    public void TestSetData2()
    {
        string urlData = $"{url}/TestData.json?auth={secret}";
        RestClient.Put<TestData>(urlData, testData).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log("Error On Set to Server");
        });
    }
    
    public void TestGetData2()
    {
        string urlData = $"{url}/TestData.json?auth={secret}";
        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);

            testnum = jsonNode["no"];
        }).Catch(error =>
        {
            Debug.Log("Error to Get Data");
        });
    }

    public void SetLocalDataToDatabase()
    {
        string urlData = $"{url}/ranking.json?auth={secret}";
        RestClient.Put<Ranking>(urlData, ranking).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log("Error on set to server");
        });
    }

    public void AddData()
    {
        string urlData = $"{url}/ranking/playerData.json?auth={secret}";
        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);
            
            string urlPlayerData = $"{url}/ranking/playerData/{jsonNode.Count}.json?auth={secret}";

            RestClient.Put<PlayerData>(urlPlayerData, currantPlayerData).Then(response =>
            {
                Debug.Log("Upload Data Complete");
            }).Catch(error =>
            {
                Debug.Log("Error On Set to Server");
            });
        }).Catch(error =>
        {
            Debug.Log("error");
        });
    }

    public void ReloadSortingData()
    {
        string urlData = $"{url}/ranking/playerData.json?auth={secret}";
        
        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);

            ranking = new Ranking();
            ranking.playerData = new List<PlayerData>();
            for (int i = 0; i < jsonNode.Count; i++)
            {
                ranking.playerData.Add(new PlayerData(jsonNode[i]["noNumber"],
                    jsonNode[i]["playerName"], jsonNode[i]["survivalWeek"],
                    jsonNode[i]["conditionEnd"]));
            }

            CalculateRankFromWeek();

            string urlPlayerData = $"{url}/ranking.json?auth={secret}";
            
            RestClient.Put<Ranking>(urlPlayerData, ranking).Then(response =>
            {
                Debug.Log("Upload Data Complete");
                resultManager.playerData = ranking.playerData;
                resultManager.ReloadRankData();
                //FindYourDataInRanking();
;           }).Catch(error =>
            {
                Debug.Log("Error On Set to Server");
            });
        }).Catch(error =>
        {
            Debug.Log("Error ");
        });
    }
}
