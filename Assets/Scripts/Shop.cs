using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GM gm;
    public GameObject player;
    public GameObject shopUI;
    public Text loot;
    public Button healthBtn;

    private int healthCost = 500;

    private void OnEnable()
    {
        PauseMenu.GameIsPaused = true;
        Time.timeScale = 0f;

        gm = GameObject.Find("GM").GetComponent<GM>();
    }

    private void Update()
    {
        healthBtn.interactable = !Player.AtMaxHealth && gm.loot >= healthCost;
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

    public void Continue()
    {
        shopUI.SetActive(false);
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
