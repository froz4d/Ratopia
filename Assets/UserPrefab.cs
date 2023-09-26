using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserPrefab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Order;
    [SerializeField] private Image PlayerProfile;
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private TextMeshProUGUI PlayerScore;

    private Player User;
    public void Create(Player userinfo,int OrderInput)
    {
        User = userinfo;
        Order.text = OrderInput.ToString();
        PlayerProfile.sprite = userinfo.PlayerProfile;
        PlayerName.text = userinfo.PlayerName;
        PlayerScore.text = userinfo.PlayerScore.ToString();
    }
}
