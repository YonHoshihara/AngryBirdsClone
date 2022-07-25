using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;
     Monster_Random[]  _monsters;
     Monster_Boss[] _monsterBoss;

    void OnEnable()
    {
        //apresenta erro!
       _monsters = FindObjectsOfType<Monster_Random>();
       _monsterBoss = FindObjectsOfType<Monster_Boss>();
    }


    void Update()
    {
        if(MonsterAreAllDead())
        {
            GoToNextLevel();
        }
    }

    void GoToNextLevel()
    {
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
        foreach (var monsterBoss in _monsterBoss)
        {
            if (monsterBoss.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
