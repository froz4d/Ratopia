using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TranManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TurnText;
    [SerializeField] private TextMeshProUGUI CSText;
    [SerializeField] private GameObject TranPanel;
    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    public static TranManager current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        panelCanvasGroup = TranPanel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = TranPanel.AddComponent<CanvasGroup>();
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OpenTranPanelWithFade()
    {
        StartCoroutine(FadeInPanel());
    }

    public void CloseTranPanelWithFade()
    {
        StartCoroutine(FadeOutPanel());
    }

    IEnumerator FadeInPanel()
    {
        TurnText.text = GameManager.CurrentTurn.ToString() + " WEEKS";
       CSText.text = $"Money: {GameManager.CurrentMoney.ToString() }\nHappiness: {GameManager.CurrentHappiness.ToString() }\nPowerArmy: {GameManager.CurrentPower.ToString() }\nStability: {GameManager.CurrentStability.ToString() }";
        TranPanel.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            panelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelCanvasGroup.alpha = 1f; // Ensure it's fully visible
    }

    IEnumerator FadeOutPanel()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            panelCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelCanvasGroup.alpha = 0f; // Ensure it's fully transparent
        TranPanel.SetActive(false);
    }
}