using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Scene _scene;
    //private static List<Scene> _allScenes;
    //private int _randomSceneIndex;
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
       // _allScenes.Add(_scene);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(_scene.buildIndex + 1);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(_scene.buildIndex);
    }
    public void AfterLevel5()
    {
        /* _randomSceneIndex = Random.Range(0, _allScenes.Count);
         SceneManager.LoadScene(_randomSceneIndex);*/
        SceneManager.LoadScene(0);
    }
}