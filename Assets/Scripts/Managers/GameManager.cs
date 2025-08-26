using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerMoney = 5000;
    public event Action OnMoneyChanged;
    
    private const string MONEY_KEY = "PlayerMoney";
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadMoney();
        OnMoneyChanged?.Invoke();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SaveMoney();
            Instance = null;
        }
    }
    private void OnApplicationQuit()
    {
        SaveMoney();
        Instance = null;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveMoney();
    }

    #region Money Management
    /// <summary>
    /// ลดเงิน Player
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceMoney(int amount)
    {
        playerMoney -= amount;
        if (playerMoney < 0) playerMoney = 0;
        
        OnMoneyChanged?.Invoke();
        SaveMoney();
    }

    /// <summary>
    /// เช็คว่าตังพอมั้ย
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool HasEnoughMoney(int amount)
    {
        return playerMoney >= amount;
    }
    #endregion

    #region Save/Load Money
    void SaveMoney()
    {
        PlayerPrefs.SetInt(MONEY_KEY, playerMoney);
        PlayerPrefs.Save();
    }

    void LoadMoney()
    {
        playerMoney = PlayerPrefs.GetInt(MONEY_KEY, 5000);
    }

    public void ResetMoney()
    {
        PlayerPrefs.DeleteKey(MONEY_KEY);
        playerMoney = 5000;
        OnMoneyChanged?.Invoke();
    }
    #endregion
}