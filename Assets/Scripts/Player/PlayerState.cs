using UnityEngine;

public delegate void IntChangeHandler(int oldValue, int newValue);

public class PlayerState
{
    public event IntChangeHandler OnHealthChanged;
    public event IntChangeHandler OnScoreChanged;

    private int health;
    private int score;

    public int Score
    {
        get => score;
        set { int oldHeath = score; score = value; OnScoreChanged?.Invoke(oldHeath, value); }
    }
    public int Health
    {
        get => health;
        set { int oldHeath = health; health = Mathf.Max(value, 0); OnHealthChanged?.Invoke(oldHeath, value); }
    }

    public PlayerState(int health)
    {
        Reset(health);
    }

    public void Reset(int startingHealth)
    {
        Health = startingHealth;
    }
}