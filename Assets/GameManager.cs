using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Zones")]
    public GameObject Zone1;
    public GameObject Zone2;
    public GameObject Zone3;

    [Header("Teleportation Area")]
    public GameObject TP_Area1;
    public GameObject TP_Area2;

    // Internal lock state for zones
    private bool zone1Locked = false;
    private bool zone2Locked = true;
    private bool zone3Locked = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    void Start()
    {
        // On game start, zone 2 and 3 should be locked, zone 1 open.
        zone1Locked = false;
        zone2Locked = true;
        zone3Locked = true;
    }

    // Public query to check whether a zone is locked
    public bool IsZoneLocked(int zoneIndex)
    {
        switch (zoneIndex)
        {
            case 1: return zone1Locked;
            case 2: return zone2Locked;
            case 3: return zone3Locked;
            default: return true;
        }
    }

    // Called by zone triggers when the player enters a zone
    public void OnZoneEntered(int zoneIndex)
    {
        if (IsZoneLocked(zoneIndex))
        {
            Debug.Log($"Zone {zoneIndex} is locked. Entry ignored.");
            return;
        }

        Debug.Log($"Player entered zone {zoneIndex}.");

        // Handle unlocking/locking logic per requirements
        if (zoneIndex == 1)
        {
            // Entering zone 1 unlocks zone 2, then lock zone1 to prevent double-unlock
            zone2Locked = false;
            zone1Locked = true;
            Debug.Log("Zone 2 unlocked. Zone 1 locked to prevent re-triggering.");
        }
        else if (zoneIndex == 2)
        {
            // Entering zone 2 unlocks zone 3, then lock zone2
            zone3Locked = false;
            zone2Locked = true;
            Debug.Log("Zone 3 unlocked. Zone 2 locked to prevent re-triggering.");
        }
        else if (zoneIndex == 3)
        {
            // Final zone entered. Lock it to avoid double triggers (optional)
            zone3Locked = true;
            Debug.Log("Final zone entered. Zone 3 locked.");
            // Unlock teleportation area 1 when zone 3 is completed
            SetTeleportAreaEnabled(TP_Area1, true);
            Debug.Log("TP_Area1 unlocked (teleportation enabled).");
        }
    }

    // Enable/disable components related to teleportation on the given area GameObject.
    private void SetTeleportAreaEnabled(GameObject area, bool enabled)
    {
        if (area == null) return;

        var mbs = area.GetComponents<MonoBehaviour>();
        foreach (var mb in mbs)
        {
            if (mb == null) continue;
            var tname = mb.GetType().Name.ToLower();
            if (tname.Contains("teleport") || tname.Contains("teleportarea") || tname.Contains("teleporationarea") || tname.Contains("teleportation"))
            {
                mb.enabled = enabled;
            }
        }
    }

    // Public helper to unlock TP area 2 when player lands on TP_Area1
    public void OnPlayerTeleportedToArea(int areaIndex)
    {
        if (areaIndex == 1)
        {
            SetTeleportAreaEnabled(TP_Area2, true);
            Debug.Log("TP_Area2 unlocked after player teleported to TP_Area1.");
        }
    }
}
