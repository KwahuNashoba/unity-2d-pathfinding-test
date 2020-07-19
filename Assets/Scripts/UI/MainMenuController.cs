using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private Button buttonPlay;

    private void Start()
    {
        // quick and dirty
        buttonPlay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(gameSceneIndex);
        });
    }
}
