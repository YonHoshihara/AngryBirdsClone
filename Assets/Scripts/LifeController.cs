using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int _life = 3;

    [SerializeField]
    private float m_DelayToCallGameOver;

    private Image uiBird1;
    private Image uiBird2;
    private Image uiBird3;
    private Monster_Random[] _monsters;
    private bool m_LevelClear = false;
    private bool m_PlayerWin = false;
    public static LifeController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster_Random>();
    }

    private void Update()
    {
        if (MonsterAreAllDead() && !m_LevelClear)
        {
            m_LevelClear = true;
            m_PlayerWin = true;
            ScreenController.Instance.CallNewScreen((int)ScreenController.m_Screens.WinScreen);
        }
    }

    public void Life()
    {
        _life--;

        if (_life <=0 && MonsterAreAllDead())
        {
            m_PlayerWin = true;
            ScreenController.Instance.CallNewScreen((int)ScreenController.m_Screens.WinScreen);
            Debug.Log("player Win");
        }

        if (!m_PlayerWin)
        {
            switch (_life)
            {
                case 2:
                    //uiBird3.color = Color.gray; 
                    break;
                case 1:
                    //uiBird2.color = Color.gray; 
                    break;
                case 0:
                    //uiBird1.color = Color.gray;
                    Invoke("LoadDefeat", m_DelayToCallGameOver);
                    break;
            }
        }
       
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

    public void LoadDefeat()
    {
        ScreenController.Instance.CallNewScreen((int) ScreenController.m_Screens.GameOverScreen);
    }

    public bool GetPlayerWinStatus() {
    
        return m_PlayerWin;
    
    }
}
