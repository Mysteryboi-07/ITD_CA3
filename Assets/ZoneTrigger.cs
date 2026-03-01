using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZoneTrigger : MonoBehaviour
{
    [Tooltip("Set to 1, 2 or 3 depending on which zone this trigger represents.")]
    public int zoneIndex = 1;

    [Tooltip("Tag used to identify the player collider (default 'Player').")]
    public string playerTag = "Player";

    void Reset()
    {
        // Ensure collider is trigger by default when added.
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager instance not found. Attach GameManager to the scene.");
            return;
        }

        GameManager.Instance.OnZoneEntered(zoneIndex);
    }
}
