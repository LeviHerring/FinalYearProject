using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuDodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float detectionRadius = 5f;
    public float jumpHeight = 3f;
    public float jumpDuration = 0.5f;

    private bool hasDodged = false;
    private List<Transform> dodgedEmus = new List<Transform>();
    private FinalCutsceneManager cutsceneManager;
    private GameObject bullet;

    void Start()
    {
        cutsceneManager = FindObjectOfType<FinalCutsceneManager>();
    }

    void Update()
    {
        if (!hasDodged)
        {
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            if (bullet == null) return;

            foreach (GameObject emuObj in cutsceneManager.emus)
            {
                float dist = Vector2.Distance(emuObj.transform.position, bullet.transform.position);
                if (dist <= detectionRadius && !dodgedEmus.Contains(emuObj.transform))
                {
                    dodgedEmus.Add(emuObj.transform);
                    StartCoroutine(JumpEmu(emuObj.transform));
                }
            }

            if (dodgedEmus.Count > 0)
            {
                hasDodged = true;
                StartCoroutine(HandleReturn());
            }
        }
    }

    IEnumerator JumpEmu(Transform emu)
    {
        Vector3 startPos = emu.position;
        Vector3 peakPos = startPos + new Vector3(0, jumpHeight, 0);

        float timer = 0f;
        while (timer < jumpDuration)
        {
            emu.position = Vector3.Lerp(startPos, peakPos, timer / jumpDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < jumpDuration)
        {
            emu.position = Vector3.Lerp(peakPos, startPos, timer / jumpDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        emu.position = startPos;
    }

    IEnumerator HandleReturn()
    {
        // Let jump finish first
        yield return new WaitForSeconds(jumpDuration * 2 + 0.2f);

        if (cutsceneManager != null)
        {
            cutsceneManager.TriggerEmuRush();
        }

        // Then delay and trigger soldier run
        yield return new WaitForSeconds(1f); // this is the delay before soldier flee

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