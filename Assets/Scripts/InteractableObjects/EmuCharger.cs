using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmuCharger : MonoBehaviour
{
    public float chargeSpeed = 5f;
    public Transform chargeTarget; // Set this to where they should charge to
    private bool isCharging = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isCharging)
        {
            // Move toward target using local or world direction
            transform.position = Vector3.MoveTowards(transform.position, chargeTarget.position, chargeSpeed * Time.deltaTime);
        }
    }

    public void StartCharge()
    {
        isCharging = true;
        animator.SetTrigger("Run"); // This plays your shared run/charge animation
    }
}
