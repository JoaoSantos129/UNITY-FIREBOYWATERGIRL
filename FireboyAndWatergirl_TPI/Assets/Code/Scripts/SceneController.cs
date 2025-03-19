using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    private int doorsOccupied = 0; // Track how many doors are occupied

    private void Awake()
    {
        // Set screen width and height pixels. Set to fullscreen or not
        // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Screen.SetResolution.html
        bool fullscreen = true; 

        Screen.SetResolution(1920, 1080, fullscreen);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DoorEntered()
    {
        doorsOccupied++;
        if (doorsOccupied >= 2) // Both doors occupied
        {
            NextLevel();
        }
    }

    public void DoorExited()
    {
        doorsOccupied--;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
