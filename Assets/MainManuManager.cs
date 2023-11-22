using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManuManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingGamePanel;
    [SerializeField] private GameObject MainManuManagerPanel;
    // Start is called before the first frame update
    void Start()
    {
        OpenPanelMainManuManagerPanel();
    }

    public void LoadGameScene()
    {
        closePanelMainManuManagerPanel();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void closePanelSettingGamePanel()
    {
        SettingGamePanel.SetActive(false);
    }

    public void OpenPanelSettingGamePanel()
    {
        SettingGamePanel.SetActive(true);
    }
    public void closePanelMainManuManagerPanel()
    {
        MainManuManagerPanel.SetActive(false);
    }

    public void OpenPanelMainManuManagerPanel()
    {
        MainManuManagerPanel.SetActive(true);
    }
}
