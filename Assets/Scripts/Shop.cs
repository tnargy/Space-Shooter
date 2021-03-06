﻿using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GM gm;
    public GameObject player;
    public GameObject shopUI;
    public Text loot;
    public Button healthBtn;
    public Button fasterBtn;
    public Button strongerBtn;
    public Button spreadBtn;
    public Button shieldBtn;

    private const int healthCost = 500;
    private const int fasterCost = 1000;
    private const int strongerCost = 2000;
    private const int spreadCost = 5000;
    private const int shieldCost = 10000;

    private void OnEnable()
    {
        PauseMenu.GameIsPaused = true;
        Time.timeScale = 0f;

        gm = GameObject.Find("GM").GetComponent<GM>();
    }

    private void Update()
    {
        healthBtn.interactable = !Player.AtMaxHealth && gm.loot >= healthCost;
        fasterBtn.interactable = gm.loot >= fasterCost;
        strongerBtn.interactable = gm.loot >= strongerCost;
        spreadBtn.interactable = gm.loot >= spreadCost;
        shieldBtn.interactable = gm.loot >= shieldCost;
        loot.text = "Loot: " + gm.loot.ToString();
    }

    public void Health()
    {
        if (!Player.AtMaxHealth)
        {
            player.SendMessage("IncreaseHealth", 1);
            gm.Loot(-healthCost);
        }
    }

    public void Faster()
    {
        player.SendMessage("Faster");
        gm.Loot(-fasterCost);
    }   
    
    public void Stronger()
    {
        player.SendMessage("Stronger");
        gm.Loot(-strongerCost);
    }

    public void Spread()
    {
        player.SendMessage("SpreadFire", true);
        gm.Loot(-spreadCost);
    }

    public void Shield()
    {
        player.SendMessage("ShieldsUp");
        gm.Loot(-shieldCost);
    }

    public void Continue()
    {
        shopUI.SetActive(false);
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
