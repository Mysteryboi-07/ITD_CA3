using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TeleportAreaTrigger : MonoBehaviour
{
    [Tooltip("Index of this teleport area (1 or 2).")]
    public int areaIndex = 1;

    [Tooltip("Tag used to identify the player collider (default 'Player').")]
    public string playerTag = "Player";

    void Reset()
    {
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

        // Notify GameManager that player has teleported/entered this area
        GameManager.Instance.OnPlayerTeleportedToArea(areaIndex);
    }
}
