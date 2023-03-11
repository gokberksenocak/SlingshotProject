using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    private int _randomIndex;

    public void LevelStartControl()
    {
        if (!PlayerPrefs.HasKey("LevelIndex"))
        {
            PlayerPrefs.SetInt("LevelIndex", 0);
            int currentLevel = PlayerPrefs.GetInt("LevelIndex");
            PlayerPrefs.SetInt("LevelIndex", currentLevel + 1);
        }

        if (PlayerPrefs.GetInt("LevelIndex") == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 2)
        {
            SceneManager.LoadScene(2);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 3)
        {
            SceneManager.LoadScene(3);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 4)
        {
            SceneManager.LoadScene(4);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 5)
        {
            SceneManager.LoadScene(5);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") > 5)
        {
            _randomIndex=PlayerPrefs.GetInt("Random");
            SceneManager.LoadScene(_randomIndex);
        }
    }


    public void LevelNextControl()
    {
        if (!PlayerPrefs.HasKey("LevelIndex"))
        {
            PlayerPrefs.SetInt("LevelIndex", 0);
        }
        int currentLevel = PlayerPrefs.GetInt("LevelIndex");
        PlayerPrefs.SetInt("LevelIndex", currentLevel + 1);

        if (PlayerPrefs.GetInt("LevelIndex") == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 2)
        {
            SceneManager.LoadScene(2);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 3)
        {
            SceneManager.LoadScene(3);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 4)
        {
            SceneManager.LoadScene(4);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 5)
        {
            SceneManager.LoadScene(5);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") > 5)
        {
            _randomIndex = Random.Range(2, 6);
            SceneManager.LoadScene(_randomIndex);
            PlayerPrefs.SetInt("Random", _randomIndex);
        }
    }

    public void LevelRetryControl()
    {
        if (PlayerPrefs.GetInt("LevelIndex") == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 2)
        {
            SceneManager.LoadScene(2);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 3)
        {
            SceneManager.LoadScene(3);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 4)
        {
            SceneManager.LoadScene(4);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") == 5)
        {
            SceneManager.LoadScene(5);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") > 5)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}