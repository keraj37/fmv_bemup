using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public GameObject menuPanel;
    public GameObject lifeBarsPanel;
    public GameObject donatePanel;
    public GameObject gameOverPanel;
    public FightManager fightManager;

    public Text txt_won;

    void Awake()
    {
        instance = this;
        Switch(true);
    }

    public void Switch(bool isOn)
    {
        fightManager.Switch(!isOn);
        lifeBarsPanel.SetActive(!isOn);
        donatePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        menuPanel.SetActive(isOn);
        SoundManager.SetMusicVolume(isOn ? 0.2f : 0.03f);
    }

    public void ShowDonatePanel()
    {
        donatePanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public static void ShowGameOver(PlayerController wonPlayer)
    {
        instance.txt_won.text = wonPlayer.name + " won!";
        instance.gameOverPanel.SetActive(true);
        instance.StartCoroutine(instance.BackToMenu());
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2.5f);
        Switch(true);
    }
}
