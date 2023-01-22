using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public PlayerInput Controls;
    public GameObject FinishEffect;
    
    public enum State
    {
        Playing,
        Won,
        Loss,
    }
    
    public State CurrentState { get; private set; }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt("LevelIndex", 0);
        private set
        {
            PlayerPrefs.SetInt("LevelIndex", value);
            PlayerPrefs.Save();
        }
    }
    
    public void OnPlayerDied()
    {
        if (CurrentState != State.Playing) return;

        CurrentState = State.Loss;
        Controls.enabled = false;
        Debug.Log("Game Over");
        ReloadLevel();
    }

    public void OnPlayerReachedFinish()
    {
        if(CurrentState != State.Playing) return;

        CurrentState = State.Won;
        Controls.enabled = false;
        LevelIndex++;
        Debug.Log("YOU WIN");
        FinishEffect.GetComponent<FinishEffect>().PlayEffect();
        StartCoroutine(ReloadCoroutine());
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ReloadLevel();
    }
}
