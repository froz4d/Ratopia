using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    public GameObject resultDataPrefab;
    public Transform resultPanel;

    public List<PlayerData> playerData = new List<PlayerData>();
    public List<GameObject> createdPlayerData = new List<GameObject>();



    private void Start()
    {
        CreateResultData();
    }


    public void CreateResultData()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            GameObject resultObject = Instantiate(resultDataPrefab, resultPanel);
            ResultData resultData = resultObject.GetComponent<ResultData>();
            resultData.playerData = new PlayerData(playerData[i].noNumber, playerData[i].playerName,
                playerData[i].survivalWeek, playerData[i].conditionEnd);
            resultData.UpdateData();
            createdPlayerData.Add(resultObject);
        }
    }


    private void ClearResultData()
    {
        foreach (GameObject createdData in createdPlayerData)
        {
            Destroy(createdData);
        }
    }
    
    
    [ContextMenu("Reload")]
    public void ReloadRankData()
    {
        ClearResultData();
        CreateResultData();
    }
    
}
