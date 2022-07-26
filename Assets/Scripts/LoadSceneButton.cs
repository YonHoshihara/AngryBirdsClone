using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private int m_SceneId;
    
    [SerializeField]
    private string m_SceneName;

    public void LoadNewSceneById()
    {
        SceneManager.LoadScene(m_SceneId);
    }

    public void LoadNewSceneByName()
    {
        SceneManager.LoadScene(m_SceneName);
    }

}
