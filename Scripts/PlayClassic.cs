using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayClassic : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(1);
    }
}
