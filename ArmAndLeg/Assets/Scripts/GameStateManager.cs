using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Combat,
    Credits,
}

public class GameStateManager : MonoBehaviour
{
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
        if (combatRound == null)
        {

            combatRound = new CombatRound(2);
        }
        else
        {
            int roundNumber = combatRound.roundNumber;
            combatRound = new CombatRound(roundNumber + 1);
            if (roundNumber > 5)
            {
                ToCredits();
                return;
            }
        }

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
        if (Input.GetKeyDown(KeyCode.F1))
            ToCombat();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public CombatRound combatRound;

    


}

public class CombatRound
{
    public CombatRound(int round)
    {
        roundNumber = round;
        enemyCount = roundNumber * roundNumber;
    }

    public int roundNumber;

    public int enemyCount;
}
