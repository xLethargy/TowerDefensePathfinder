using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] TextMeshProUGUI goldBalanceText;
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        goldBalanceText.text = $"Gold: {currentBalance}";
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (currentBalance < 0)
        {
            // Lose the game
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
