using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldDropped = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;
    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void DropGold()
    {
        if (bank == null) { return; }

        bank.Deposit(goldDropped); 
    }

    public void StealGold()
    {
        if (bank == null) { return; }

        bank.Withdraw(goldPenalty); 
    }
}
