using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    [SerializeField] 
    string _nextLevelName;
    private Monster_Random[]  _monsters;
    private bool m_LevelClear = false;

    void OnEnable()
    {
        //apresenta erro!
       _monsters = FindObjectsOfType<Monster_Random>();
    }


    void Update()
    {
        if(MonsterAreAllDead() && !m_LevelClear)
        {
            m_LevelClear = true;
            GoToNextLevel();
        }
    }

    void GoToNextLevel()
    {
        //ScreenController.Instance.CallNewScreen((int)ScreenController.m_Screens.WinScreen);
        Debug.Log("Go to Level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    bool MonsterAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
