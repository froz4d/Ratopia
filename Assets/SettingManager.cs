using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject Devlog;
    [SerializeField] private GameObject volune; 
    [SerializeField] private GameObject AduioManager;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Devlog.GetComponent<Toggle>().isOn)
        {
            GameManager.ShowDevLog = true;
        }
        else
        {
            GameManager.ShowDevLog = false;
        }
        if (volune.GetComponent<Toggle>().isOn)
        {
            AduioManager.SetActive(true);
        }
        else
        {
            AduioManager.SetActive(false);
        }
    }

    public void closePanel()
    {
        SettingPanel.SetActive(false);
    }
    public void OpenPanel()
    {
        SettingPanel.SetActive(true);
    }
}
