using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushGun : MonoBehaviour
{
    public float shootRange = 5f;
    public LayerMask emuLayer;
    public Transform gunTip;

    public int kills = 0;
    private int maxKills = 12;
    private bool isJammed = false;

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
                hitEmu = true;
                Debug.Log("Emu eliminated. Total kills: " + kills);
                break; // Only kill one per shot
            }
        }

        if (!hitEmu)
        {
            Debug.Log("No emus in range.");
        }

        if (kills >= maxKills)
        {
            isJammed = true;
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
}