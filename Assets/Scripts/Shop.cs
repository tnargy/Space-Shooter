using UnityEngine;
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

    private const int healthCost = 500;
    private const int fasterCost = 1000;
    private const int strongerCost = 2000;

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

    public void Continue()
    {
        shopUI.SetActive(false);
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
