using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void FirstScene()
    {
        SceneManager.LoadScene("First");
    }
    public void LobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void MercernaryGuildScene()
    {
        SceneManager.LoadScene("MercenaryGuild");
    }
    public void ShopScene()
    {
        SceneManager.LoadScene("Shop");
    }
    public void DungeonScene()
    {
        SceneManager.LoadScene("Dungeon");
    }
    public void DungeonFightScene()
    {
        SceneManager.LoadScene("DungeonFight");
    }
}
