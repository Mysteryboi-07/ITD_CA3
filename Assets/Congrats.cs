using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Congrats : MonoBehaviour
{
    [Tooltip("The GameObject representing the end zone. Will be disabled when the player enters.")]
    public GameObject endZone;

    [Tooltip("TMP text object that displays the 'End' message.")]
    public TMP_Text endMessageText;

    [Tooltip("Panel that shows the congratulations UI. Should be inactive at start.")]
    public GameObject congratsPanel;

    [Tooltip("Name of the scene to load when the player presses the return button.")]
    public string sampleSceneName = "SampleScene";

    private Button returnButton;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    void Start()
    {
        if (congratsPanel != null)
            congratsPanel.SetActive(false);

        if (congratsPanel != null)
            returnButton = congratsPanel.GetComponentInChildren<Button>();

        if (returnButton != null)
            returnButton.onClick.AddListener(ReturnToSampleScene);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // disable specified objects
        if (endZone != null)
            endZone.SetActive(false);

        if (endMessageText != null)
            endMessageText.gameObject.SetActive(false);

        // show congrats UI
        if (congratsPanel != null)
            congratsPanel.SetActive(true);
    }

    void ReturnToSampleScene()
    {
        if (!string.IsNullOrEmpty(sampleSceneName))
            SceneManager.LoadScene(sampleSceneName);
    }
}