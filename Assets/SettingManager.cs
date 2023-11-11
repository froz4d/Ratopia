using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject Devlog;
    [SerializeField] private Slider volune;
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
