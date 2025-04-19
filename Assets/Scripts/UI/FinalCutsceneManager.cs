using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalCutsceneManager : MonoBehaviour
{
    [Header("Scene Objects")]
    public GameObject[] emus;
    public GameObject[] soldiers;
    public GameObject bullet;
    public Transform bulletSpawn;
    public Animator fadeAnimator;

    [Header("UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI shootText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup statsGroup;

    private bool canShoot = false;
    private bool shotFired = false;

    void Start()
    {
        shootText.gameObject.SetActive(false);
        statsGroup.alpha = 0;
        fadeAnimator.SetTrigger("Idle");

        StartCoroutine(CutsceneSequence());
    }

    void Update()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.Space) && !shotFired)
        {
            shotFired = true;
            canShoot = false;
            shootText.gameObject.SetActive(false);
            StartCoroutine(ShootBullet());
        }
    }

    IEnumerator CutsceneSequence()
    {
        // Show "LAST STAND"
        titleText.text = "LAST STAND";
        titleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        titleText.gameObject.SetActive(false);

        // Show emus and soldiers
        foreach (var soldier in soldiers)
            soldier.SetActive(true);
        foreach (var emu in emus)
            emu.SetActive(true);

        yield return new WaitForSeconds(1f);

        ShowShootText();
    }

    void ShowShootText()
    {
        shootText.text = "Press Space to shoot";
        shootText.gameObject.SetActive(true);
        canShoot = true;
    }

    IEnumerator ShootBullet()
    {
        // Fire weak bullet
        GameObject b = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(3f, 0.5f); // Weak, slow bullet

        // EmuDodge will take over from here (dodge + restore + call OnEmusChargeFinished)
        yield return null;
    }

    // Called by EmuDodge AFTER all emus have charged and returned
    public void OnEmusChargeFinished()
    {
        StartCoroutine(RunawaySequence());
    }

    public void TriggerEmuRush()
    {
        Debug.Log("EMUS ARE CHARGING!!");

        foreach (GameObject emu in emus)
        {
            EmuCharger charger = emu.GetComponent<EmuCharger>();
            if (charger != null)
                charger.StartCharge(); // Add animation/audio/etc in EmuCharger
        }
    }

    IEnumerator RunawaySequence()
    {
        // Soldiers flee
        foreach (GameObject soldier in soldiers)
        {
            Animator anim = soldier.GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Runaway");
        }

        yield return new WaitForSeconds(2f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2f);

        // Show stats
        statsGroup.alpha = 1;
        titleText.text = "The Army Retreats";
        subtitleText.text = "Emus killed: 986 / Estimated emus: 20,000+\nSoldiers injured: 0\nMorale: -1000";
        titleText.gameObject.SetActive(true);
        subtitleText.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);

        // Final subtitle
        titleText.text = "";
        subtitleText.text = "With no choice, the government turns to... bounty hunters.";
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("BountyScene"); // Replace with actual next scene name
    }
}
