using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    public int score = 0;
    public int collectedScore = 0;
    public int buyCost = 20;
    public int upgradeCost = 50;
    public int incomeCost = 100;
    public int sizeUpThreshold = 1000;
    public int nextTrackCost = 7000;
    public int prevTrackCost = 7000;
    public Button buyButton, upgradeButton, sizeUpButton, incomeButon, nextTrackButton, prevTrackButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int level) => score += level * 2;

    public void BuyRunner()
    {
        if (score >= buyCost)
        {
            score -= buyCost;
            buyCost += 23;
            CharacterManager.Instance.SpawnRunner();
            AudioManager.Instance.PlayBuySound();

            UIManager.Instance.UpdateUI();
            UpdateButtonState();
        }
    }

    public void Upgrade()
    {
        if (score >= upgradeCost && CharacterManager.Instance.CanUpgrade())
        {
            score -= upgradeCost;
            upgradeCost += 25;
            CharacterManager.Instance.UpgradeRunner();
            AudioManager.Instance.PlayUpgradeSound();
            UIManager.Instance.UpdateUI();
            UpdateButtonState();
        }
    }

    public void CollectIncome()
    {
        if (score >= incomeCost)
        {
            collectedScore += incomeCost;
            score -= incomeCost;
            incomeCost += 50;
            UIManager.Instance.UpdateUI();
            UpdateButtonState();

        }
    }

    public void SizeUp()
    {
        if (score >= sizeUpThreshold)
        {
            score -= sizeUpThreshold;
            sizeUpThreshold *= 2; 
            if (Map.Instance.CurrentMapInstance != null)
            {
                MapData map = Map.Instance.CurrentMapInstance.GetComponent<MapData>();
                map.UnlockTrack();
            }
            AudioManager.Instance.PlayUpgradeSound();
            UIManager.Instance.UpdateUI();
            UpdateButtonState();
        }
    }
    public void UpdateButtonState()
    {
        var map = Map.Instance.CurrentMapInstance.GetComponent<MapData>();

        buyButton.interactable = (score >= buyCost);
        incomeButon.interactable = (score >= incomeCost);
        sizeUpButton.interactable = (score >= sizeUpThreshold);
        nextTrackButton.interactable = (score >= nextTrackCost);
        prevTrackButton.interactable = (score >= prevTrackCost);

        bool hasTrackWith3Char = false;
        bool hasEnoughScoreToUpgrade = false;

        for (int i = 0; i < map.unlockedTracks - 1; i++)
        {
            var track = CharacterManager.Instance.GetTrack(i);
            if (track.Count >= 3)
            {
                hasTrackWith3Char = true;
                if (score >= upgradeCost)
                {
                    hasEnoughScoreToUpgrade = true;
                    break;
                }
            }
        }

        upgradeButton.gameObject.SetActive(hasTrackWith3Char);
        upgradeButton.interactable = hasEnoughScoreToUpgrade;

        if (map.unlockedTracks >= map.trackObjects.Length)
        {
            sizeUpButton.gameObject.SetActive(false);
        }

        bool hasCharacterInTrack3 = CharacterManager.Instance.GetTrack(2).Count > 0;
        bool hasNextMap = Map.Instance.currentMapIndex < Map.Instance.mapPrefabs.Length - 1;
        bool hasPrevMap = Map.Instance.currentMapIndex > 0;

        nextTrackButton.gameObject.SetActive(hasCharacterInTrack3 && hasNextMap);
        prevTrackButton.gameObject.SetActive(hasCharacterInTrack3 && hasPrevMap);

        nextTrackButton.interactable = hasCharacterInTrack3 && hasNextMap && (score >= nextTrackCost);
        prevTrackButton.interactable = hasCharacterInTrack3 && hasPrevMap && (score >= prevTrackCost);

        SaveManager.Instance.SaveGame();
    }


}
