using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScreenManagerScript : MonoBehaviour
{
    private Stack<string> screenHistory = new Stack<string>();

    // Singleton instance
    public static ScreenManagerScript Instance;

    private void Awake()
    {
        // Ensure there is only one instance of this script
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string screenName)
    {
        // Get the current active scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Avoid pushing the same screen twice consecutively
        if (screenHistory.Count == 0 || screenHistory.Peek() != currentSceneName)
        {
            screenHistory.Push(currentSceneName);
        }

        // Load the new scene
        SceneManager.LoadScene(screenName);
    }

    public void GoBack()
    {
        if (screenHistory.Count > 0)
        {
            // Pop the last scene from the stack and load it
            string previousScreen = screenHistory.Pop();
            SceneManager.LoadScene(previousScreen);
        }
        else
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void ClearHistory()
    {
        screenHistory.Clear();
    }
}
