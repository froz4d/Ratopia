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
    [SerializeField] private Slider volunmbar;
    
    void Start()
    {
        LoadSettings();
    }
    
    void Update()
    {
     
    }

    void LoadSettings()
    {
        if (Devlog != null)
        {
            bool showDevLog = PlayerPrefs.GetInt("ShowDevLog", 0) == 1;
            Devlog.GetComponent<Toggle>().isOn = showDevLog;
            GameManager.ShowDevLog = showDevLog;
        }
        if (volune != null)
        {
            bool audioManagerActive = PlayerPrefs.GetInt("AudioManagerActive", 1) == 1;
            volune.GetComponent<Toggle>().isOn = audioManagerActive;
            AduioManager.SetActive(audioManagerActive);
        }
        if (volunmbar != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
            volunmbar.value = savedVolume;
            AduioManager.GetComponent<AudioSource>().volume = volunmbar.value;
        }
    }
    

    public void SaveSettings()
    {
        if (Devlog != null)
        {
            PlayerPrefs.SetInt("ShowDevLog", Devlog.GetComponent<Toggle>().isOn ? 1 : 0);
        }
        if (volune != null)
        {
            PlayerPrefs.SetInt("AudioManagerActive", volune.GetComponent<Toggle>().isOn ? 1 : 0);
        }
        if (volunmbar != null)
        {
            PlayerPrefs.SetFloat("Volume", volunmbar.value);
        }

        PlayerPrefs.Save();
        LoadSettings(); 
    }

    public void closePanel()
    {
        SettingPanel.SetActive(false);
        SaveSettings();
        LoadSettings();
    }

    public void OpenPanel()
    {
        SettingPanel.SetActive(true);
    }
}
