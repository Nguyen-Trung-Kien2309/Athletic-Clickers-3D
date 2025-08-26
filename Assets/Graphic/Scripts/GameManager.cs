using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int collectedScore = 0;
    public int buyCost = 20;
    public int upgradeCost = 50;
    public int incomeCost = 100;
    public int sizeUpThreshold = 1000;
    public int nextTrackCost = 7000;
    public int prevTrackCost = 7000;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        AudioManager.Instance.PlayBackgroundMusic();
        SaveManager.Instance.LoadPlayerData();
        Map.Instance.map();
        SaveManager.Instance.LoadSceneData();

        //SaveManager.Instance.DeleteAllSaveData();
        UIManager.Instance.UpdateUI();
    }

    public void AddScore(int level)
    {
        UIManager.Instance.AddScore(level);
        UIManager.Instance.UpdateUI();

    }


    //public void QuitGame()
    //{
    //    SaveManager.Instance.SaveGame();
    //}

  
}
