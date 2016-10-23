using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Combat,
    Credits,
}

public class GameStateManager : MonoBehaviour {
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void ToStart()
    {
        m_currentState = GameState.Start;
        SceneManager.LoadScene(0);
    }

    public void ToCombat()
    {
        m_currentState = GameState.Combat;
        SceneManager.LoadScene(1);
    }

    public void ToCredits()
    {
        m_currentState = GameState.Credits;
        SceneManager.LoadScene(2);
    }

    
    private GameState m_currentState;
    public GameState Current
    {
        get { return m_currentState; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ToCredits();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
