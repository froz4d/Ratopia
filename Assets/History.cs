using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [SerializeField] private Transform showAllHistoryLog;
    [SerializeField] private GameObject Display;
    
    public class LogEntry
    {
        private int _turn;
        private string stringValue;
        public readonly string OutCome;
        public bool IsDevLog;

        public LogEntry(string lastAction,bool isDevLog)
        {
            _turn = GameManager.CurrentTurn;
            stringValue = lastAction;
            IsDevLog = isDevLog;

            if (isDevLog)
            {
                OutCome = "[DevLog] : " + Turn.ToString() + " : " + stringValue;
            }
            else
            {
                OutCome = Turn.ToString() + " : " + stringValue;
            }
            
        }

        public int Turn => _turn;
    }

    public List<LogEntry> HistoryLog = new List<LogEntry>();
    
    public void Record(string LastAction)
    {
        HistoryLog.Add(new LogEntry(LastAction,false));
        RefreshRecord();
    }

    public void DevRecord(string LastAction)
    {
        HistoryLog.Add(new LogEntry(LastAction,true));
        RefreshRecord();
        //ต้องมาใส่ history
    }

    private void RefreshRecord()
    {
        //สำหรับ สร้าง Record
        
        //destory
        foreach (Transform child in showAllHistoryLog)
        {
            Destroy(child.gameObject);
        }
        
        //Re-Build
        foreach (var logEntry in HistoryLog)
        {
            if (GameManager.ShowDevLog == false && logEntry.IsDevLog)
            {
                continue;
            }
            else
            {
                TextMeshProUGUI text = Instantiate(Display,showAllHistoryLog).GetComponent<TextMeshProUGUI>();
                text.gameObject.name = logEntry.Turn.ToString();
                text.text = logEntry.OutCome;
            }
            
        }
    }
    
    [SerializeField]private Toggle toggle;
    [SerializeField]private GameObject targetObject;
    [SerializeField]private GameObject targetObject2;

    private void Start()
    {
        toggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    private void ToggleValueChanged(bool isOn)
    {
        targetObject.SetActive(isOn);
        targetObject2.SetActive(isOn);
    }
}
