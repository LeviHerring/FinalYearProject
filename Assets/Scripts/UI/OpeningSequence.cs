using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OpeningSequence : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        StartCoroutine(Opening());
    }

    IEnumerator Opening()
    {
        // Set initial alpha to 0 (fully transparent)
        Color color = text.color;
        color.a = 0;
        text.color = color;

        float duration = 5f;
        float elapsed = 0f;

        // Lerp alpha from 0 to 1 over 5 seconds
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            color.a = alpha;
            text.color = color;
            yield return null;
        }

        // Wait 2 more seconds
        yield return new WaitForSeconds(2f);

        // Load next scene (adjust index or name as needed)
        SceneManager.LoadScene(2);
    }
}