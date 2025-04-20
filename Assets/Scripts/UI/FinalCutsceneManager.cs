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

        statsGroup.alpha = 1; // Make stats visible at start
        titleText.gameObject.SetActive(true);
        titleText.text = "LAST STAND";

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
        yield return new WaitForSeconds(2f);

        // Fade out statsGroup (if it was showing something like "LAST STAND")
        StartCoroutine(FadeCanvasGroup(statsGroup, 1.5f));

        yield return new WaitForSeconds(1f); // Wait for fade

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
        GameObject b = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        b.tag = "Bullet"; // Ensure it has the correct tag for EmuDodge to find it

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(3f, 0.5f);

        yield return null;
    }

    public void TriggerEmuRush()
    {
        Debug.Log("EMUS ARE CHARGING!!");

        foreach (GameObject emu in emus)
        {
            EmuCharger charger = emu.GetComponent<EmuCharger>();
            if (charger != null)
                charger.StartCharge();
        }
    }

    public void OnEmusChargeFinished()
    {
        StartCoroutine(RunawaySequence());
    }

    IEnumerator RunawaySequence()
    {
        yield return new WaitForSeconds(1f); // Quick delay before soldiers react

        foreach (GameObject soldier in soldiers)
        {
            Runaway run = soldier.GetComponent<Runaway>();
            Animator anim = soldier.GetComponent<Animator>();

            if (run != null) run.StartRunaway();
            if (anim != null) anim.SetTrigger("Runaway");
        }

        yield return new WaitForSeconds(2f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);
        fadeAnimator.SetTrigger("Black");

        // Show stats panel
        statsGroup.alpha = 1;
        titleText.text = "The Army Retreats";
        subtitleText.text = "Emus killed: 986 / Estimated emus: 20,000+\nSoldiers injured: 0\nMorale: -1000";
        titleText.gameObject.SetActive(true);
        subtitleText.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);

        titleText.text = "";
        subtitleText.text = "With no choice, the government turns to... bounty hunters.";
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("BountyScene");
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float duration)
    {
        float startAlpha = cg.alpha;
        float time = 0f;
        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 0;
    }
}
