using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject MenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadMenuScene()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SaveGameManager.LoadGame();
    }
    public void ContinueGame()
    {
        
    }

    public void closeSettingPanel()
    {
        SettingPanel.SetActive(false);
        closeMenuPanel();
    }

    public void OpenSettingPanel()
    {
        SettingPanel.SetActive(true);
        closeMenuPanel();
    }
    public void closeMenuPanel()
    {
        MenuPanel.SetActive(false);
    }

    public void OpenMenuPanel()
    {
        MenuPanel.SetActive(true);
    }
}

