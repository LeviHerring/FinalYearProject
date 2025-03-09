using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject[] parentScenes; // Array of all the different areas in your current scene
    private int currentLevel = 0; // Index for the current area
    private bool isTransitioning = false; // Prevent switching multiple times before the transition is done

    // Update is called once per frame
    void Update()
    {
        // Trigger asset change with right mouse button (you can change the button if needed)
        if (Input.GetMouseButtonDown(1) && !isTransitioning)
        {
            ChangeAssets(); // Call to swap assets in the same scene
        }

        // Example of switching to the next scene with left mouse button
        if (Input.GetKeyDown(KeyCode.Space)) // Using Space for scene switching
        {
            ChangeScene(); // Call to change scenes
        }
    }

    // This method switches to the next scene in the build order
    void ChangeScene()
    {
        if (isTransitioning) return; // Prevent scene switch if already transitioning
        isTransitioning = true; // Set transitioning flag

        // Get the next scene index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Changing to scene " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load.");
        }

        isTransitioning = false; // Reset flag after the change
    }

    // This method changes the active area within the current scene
    void ChangeAssets()
    {
        if (isTransitioning) return; // Prevent asset change if already transitioning
        isTransitioning = true; // Set transitioning flag

        // Make sure we're not trying to go past the last area in the array
        if (currentLevel < parentScenes.Length - 1)
        {
            parentScenes[currentLevel].SetActive(false); // Deactivate current area
            currentLevel++; // Move to the next level
            parentScenes[currentLevel].SetActive(true); // Activate the new area
            Debug.Log("Changed to area " + currentLevel);
        }
        else
        {
            Debug.Log("Already at the last area.");
        }

        isTransitioning = false; // Reset flag after the change
    }
}
