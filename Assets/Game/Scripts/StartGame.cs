using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Game/Scenes/ScienceBase");
    }
}