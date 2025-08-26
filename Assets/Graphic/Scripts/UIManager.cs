using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button buyButton, upgradeButton, sizeUpButton, incomeButon, nextTrackButton, prevTrackButton;
    public TextMeshProUGUI scoreText, collectedText, buyText, upgradeText, incomeText, sizeUpText, nextTrackText, prevTrackText;

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

    public void UpdateUI()
    {
        var eco = GameManager.Instance;
        scoreText.text = Format(eco.score) + "$";
        collectedText.text = Format(eco.collectedScore) + "/SEC";
        buyText.text = $"Runner\n({Format(eco.buyCost)} $)";
        upgradeText.text = $"Meger\n({Format(eco.upgradeCost)} $)";
        incomeText.text = $"Income\n({Format(eco.incomeCost)} $)";
        sizeUpText.text = $"Size Up\n({Format(eco.sizeUpThreshold)} $)";
        nextTrackText.text = $"Next Track\n({Format(eco.nextTrackCost)} $)";
        prevTrackText.text = $"Prev Track\n({Format(eco.prevTrackCost)} $)";
        UpdateButtonState();
    }

    private string Format(int val)
    {
        if (val >= 1000000) return (val / 1000000f).ToString("0.#") + "M";
        if (val >= 1000) return (val / 1000f).ToString("0.#") + "K";
        return val.ToString();
    }

  

    public void AddScore(int level) => GameManager.Instance.score += level * 2;

    public void BuyRunner()
    {
        var eco = GameManager.Instance;
        if (eco.score >= eco.buyCost)
        {
            eco.score -= eco.buyCost;
            eco.buyCost += 23;
            CharacterManager.Instance.SpawnRunner();
            AudioManager.Instance.PlayBuySound();

            UpdateUI();
            UpdateButtonState();
        }
    }

    public void Upgrade()
    {
        var eco = GameManager.Instance;
        if (eco.score >= eco.upgradeCost && CharacterManager.Instance.CanUpgrade())
        {
            eco.score -= eco.upgradeCost;
            eco.upgradeCost += 25;
            CharacterManager.Instance.UpgradeRunner();
            AudioManager.Instance.PlayUpgradeSound();
            UpdateUI();
            UpdateButtonState();
        }
    }

    public void CollectIncome()
    {
        var eco = GameManager.Instance;
        if (eco.score >= eco.incomeCost)
        {
            eco.collectedScore += eco.incomeCost;
            eco.score -= eco.incomeCost;
            eco.incomeCost += 50;
            UpdateUI();
            UpdateButtonState();

        }
    }

    public void SizeUp()
    {
        var eco = GameManager.Instance;
        if (eco.score >= eco.sizeUpThreshold)
        {
            eco.score -= eco.sizeUpThreshold;
            eco.sizeUpThreshold *= 2;
            if (Map.Instance.CurrentMapInstance != null)
            {
                MapData map = Map.Instance.CurrentMapInstance.GetComponent<MapData>();
                map.UnlockTrack();
            }
            AudioManager.Instance.PlayUpgradeSound();
            UpdateUI();
            UpdateButtonState();
        }
    }
    public void UpdateButtonState()
    {

        var eco = GameManager.Instance;
        MapData map = Map.Instance.CurrentMapInstance?.GetComponent<MapData>();
        if (map == null) return;

        buyButton.interactable = (eco.score >= eco.buyCost);
        incomeButon.interactable = (eco.score >= eco.incomeCost);
        sizeUpButton.interactable = (eco.score >= eco.sizeUpThreshold);
        nextTrackButton.interactable = (eco.score >= eco.nextTrackCost);
        prevTrackButton.interactable = (eco.score >= eco.prevTrackCost);

        //bool hasTrackWith3Char = false;
        bool hasEnoughScoreToUpgrade = false;

        for (int i = 0; i < map.unlockedTracks - 1; i++)
        {
            if (i >= CharacterManager.Instance.TrackCount) continue;

            var track = CharacterManager.Instance.GetTrack(i);
            if (track.Count >= 3)
            {
                //hasTrackWith3Char = true;
                if (eco.score >= eco.upgradeCost)
                {
                    hasEnoughScoreToUpgrade = true;
                    break;
                }
            }
        }

        //upgradeButton.gameObject.SetActive(hasTrackWith3Char);
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

        nextTrackButton.interactable = hasCharacterInTrack3 && hasNextMap && (eco.score >= eco.nextTrackCost);
        prevTrackButton.interactable = hasCharacterInTrack3 && hasPrevMap && (eco.score >= eco.prevTrackCost);

        SaveManager.Instance.SaveGame();
        //SaveManager.Instance.DeleteAllSaveData();
    }

}
