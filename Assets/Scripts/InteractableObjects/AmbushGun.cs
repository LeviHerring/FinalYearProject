using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbushGun : MonoBehaviour
{
    public static AmbushGun Instance;

    public float shootRange = 5f;
    public LayerMask emuLayer;
    public Transform gunTip;

    public int kills = 0;
    private int maxKills = 12;
    private bool isJammed = false;

    public int nextSceneIndex = -1;

    public int emusInScene = 0;
    private bool transitioning = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        emusInScene = GameObject.FindGameObjectsWithTag("Emu").Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJammed)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isJammed)
        {
            Debug.Log("The gun is jammed! You can’t reload... and the emus are going wild!");
        }

        // If jammed AND no more emus, transition
        if (isJammed && emusInScene <= 0 && !transitioning)
        {
            transitioning = true;
            StartCoroutine(LoadNextSceneAfterDelay(2f));
        }
    }

    void Shoot()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(gunTip.position, shootRange, emuLayer);

        bool hitEmu = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Emu"))
            {
                Destroy(hit.gameObject);
                kills++;
                EmuDespawned(); // Track one less emu in scene
                hitEmu = true;
                Debug.Log("Emu eliminated. Total kills: " + kills);
                break;
            }
        }

        if (!hitEmu)
        {
            Debug.Log("No emus in range.");
        }

        if (kills >= maxKills)
        {
            isJammed = true;
            TriggerPanic(); // <- ADD THIS
        }
    }

    public void EmuEscaped()
    {
        if (!isJammed)
        {
            kills++;
            emusInScene--;
            Debug.Log("An emu escaped! Kill count increased to: " + kills + " (from escape)");

            if (kills >= maxKills)
            {
                isJammed = true;
                emusInScene--;
                Debug.Log("Gun jammed after too many missed emus!");
            }
        }
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        Debug.Log("All emus gone. Transitioning to next scene...");
        yield return new WaitForSeconds(delay);

        if (nextSceneIndex >= 0)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Next scene index not set!");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (gunTip != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gunTip.position, shootRange);
        }
    }

    public void EmuDespawned()
    {
        emusInScene = Mathf.Max(0, emusInScene - 1); // Ensure it never goes below 0
        Debug.Log("An emu despawned. Remaining: " + emusInScene);
    }

    void TriggerPanic()
    {
        Debug.Log("Gun is jammed — PANIC MODE!");

        OstrichAmbush[] allEmus = FindObjectsOfType<OstrichAmbush>();

        foreach (OstrichAmbush emu in allEmus)
        {
            emu.Panic();
        }
    }

}
