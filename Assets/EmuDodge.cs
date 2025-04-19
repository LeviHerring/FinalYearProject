using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuDodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float detectionRadius = 2f;
    public float dodgeDistance = 5f;
    public float restoreDelay = 3f;

    private bool hasDodged = false;

    private List<Transform> dodgedEmus = new List<Transform>();
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();

    private FinalCutsceneManager cutsceneManager;
    private GameObject[] emus;

    void Start()
    {
        cutsceneManager = FindObjectOfType<FinalCutsceneManager>();
        if (cutsceneManager != null)
        {
            emus = cutsceneManager.emus;
        }
        else
        {
            Debug.LogError("FinalCutsceneManager not found in the scene.");
        }
    }

    void Update()
    {
        if (!hasDodged && emus != null)
        {
            foreach (GameObject emuObj in emus)
            {
                float dist = Vector2.Distance(transform.position, emuObj.transform.position);
                if (dist <= detectionRadius)
                {
                    hasDodged = true;

                    Vector3 originalPos = emuObj.transform.position;
                    originalPositions[emuObj.transform] = originalPos;

                    // Push up or down depending on position
                    float pushDirection = (transform.position.y > originalPos.y) ? -1f : 1f;
                    Vector3 dodgePos = originalPos + new Vector3(0, dodgeDistance * pushDirection, 0);
                    emuObj.transform.position = dodgePos;

                    dodgedEmus.Add(emuObj.transform);
                }
            }

            if (hasDodged)
            {
                StartCoroutine(WaitAndReturn());
            }
        }
    }

    IEnumerator WaitAndReturn()
    {
        // Trigger emus rushing
        if (cutsceneManager != null)
        {
            cutsceneManager.TriggerEmuRush();
        }

        // Wait until emus are no longer overlapping
        float timer = 0f;
        while (timer < restoreDelay)
        {
            bool anyOverlap = false;

            foreach (var emu in dodgedEmus)
            {
                Collider2D col = emu.GetComponent<Collider2D>();
                if (col != null && Physics2D.OverlapCircle(col.bounds.center, col.bounds.extents.magnitude, LayerMask.GetMask("Default")))
                {
                    anyOverlap = true;
                    break;
                }
            }

            if (!anyOverlap)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f; // reset if overlapping again
            }

            yield return null;
        }

        // Return emus to original positions
        foreach (Transform emu in dodgedEmus)
        {
            if (originalPositions.ContainsKey(emu))
            {
                emu.position = originalPositions[emu];
            }
        }

        // Trigger the next stage of the cutscene
        if (cutsceneManager != null)
        {
            cutsceneManager.OnEmusChargeFinished();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
