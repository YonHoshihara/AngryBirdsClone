using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance { get; private set; }

    [SerializeField]
    private Animator [] m_AnimatorScreens;
    private Animator m_CurrentScreenOppened;

    public enum m_Screens
    {
        GameOverScreen,
        WinScreen
    }

    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            m_CurrentScreenOppened = null;
        }
    }

    public void CallNewScreen(int screenToOpen)
    {
        if (m_CurrentScreenOppened != null)
        {
            m_CurrentScreenOppened.SetBool("Show", false);
            m_CurrentScreenOppened.gameObject.SetActive(false);
        } 
        m_CurrentScreenOppened = m_AnimatorScreens[screenToOpen];
        m_CurrentScreenOppened.gameObject.SetActive(true);
        m_CurrentScreenOppened.SetBool("Show", true);

    }

}
