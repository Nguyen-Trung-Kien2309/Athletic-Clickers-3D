
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using TMPro;

//public class Game1 : MonoBehaviour
//{
//    public int score = 0;
//    public int collectedScore = 0;
//    public int buyCost = 20;
//    public int upgradeCost = 50;
//    public int incomeCost = 100;
//    public int sizeUpThreshold = 1000;
//    public TextMeshProUGUI scoreText, collectedScoreText;
//    public Text buyCostText, upgradeCostText, sizeupCostText, incomeCostText;
//    public Button buyButton, upgradeButton, sizeUpButton, incomeButon;
//    public GameObject characterPrefab1, characterPrefab2, characterPrefab3, characterPrefab4, characterPrefab5, characterPrefab6, characterPrefab7;
//    public Transform startPosition1, startPosition2, startPosition3, startPosition4, startPosition5, startPosition6, startPosition7;
//    public Transform[] pathPoints1, pathPoints2, pathPoints3, pathPoints4, pathPoints5, pathPoints6, pathPoints7;

//    private List<GameObject> charactersInTrack1 = new List<GameObject>();
//    private List<GameObject> charactersInTrack2 = new List<GameObject>();
//    private List<GameObject> charactersInTrack3 = new List<GameObject>();
//    private List<GameObject> charactersInTrack4 = new List<GameObject>();
//    private List<GameObject> charactersInTrack5 = new List<GameObject>();
//    private List<GameObject> charactersInTrack6 = new List<GameObject>();
//    private List<GameObject> charactersInTrack7 = new List<GameObject>();
//    public GameObject[] newTrack;
//    public int unlockedTracks = 2;
//    private bool isBoosted = false;

//    void Start()
//    {
//        for (int i = 2; i < newTrack.Length; i++)
//        {
//            newTrack[i].SetActive(false);
//        }
//        upgradeButton.gameObject.SetActive(false);
//        GameObject firstCharacter = Instantiate(characterPrefab1, startPosition1.position, Quaternion.identity);
//        firstCharacter.GetComponent<Run>().StartRunner(pathPoints1, this, 1);
//        charactersInTrack1.Add(firstCharacter);
//        UpdateUI();
//        UpdateButtonState();
//    }

//    void FixedUpdate()
//    {
//        if (Input.GetMouseButtonDown(1))
//        {
//            SetRunnerSpeed(2f);
//            isBoosted = true;
//        }
//        if (Input.GetMouseButtonUp(1) && isBoosted)
//        {
//            SetRunnerSpeed(1f);
//            isBoosted = false;
//        }

//    }

//    private void SetRunnerSpeed(float speedMultiplier)
//    {
//        List<GameObject>[] allTracks = { charactersInTrack1, charactersInTrack2, charactersInTrack3, charactersInTrack4, charactersInTrack5, charactersInTrack6, charactersInTrack7 };

//        foreach (var track in allTracks)
//        {
//            foreach (var character in track)
//            {
//                character.GetComponent<Run>().SetSpeedMultiplier(speedMultiplier);

//            }
//        }
//    }


//    public void AddScore(int level)
//    {
//        int scoreToAdd = level * 2;
//        score += scoreToAdd;
//        UpdateUI();
//        UpdateButtonState();
//    }
//    string FormatScore(int score)
//    {
//        if (score >= 1000000) return (score / 1000000f).ToString("0.#") + "M";
//        if (score >= 1000) return (score / 1000f).ToString("0.#") + "K";
//        return score.ToString();
//    }


//    public void UpdateUI()
//    {
//        scoreText.text = FormatScore(score) + "$";
//        buyCostText.text = "Runner (" + FormatScore(buyCost) + " $)";
//        upgradeCostText.text = "Meger (" + FormatScore(upgradeCost) + " $)";
//        incomeCostText.text = "Income (" + FormatScore(incomeCost) + " $)";
//        sizeupCostText.text = "Size Up (" + FormatScore(sizeUpThreshold) + " $)";
//        collectedScoreText.text = FormatScore(collectedScore) + "/SEC";
//        UpdateButtonState();
//    }

//    public void BuyCharacter()
//    {
//        if (score >= buyCost)
//        {
//            score -= buyCost;
//            buyCost += 23;

//            GameObject newCharacter = Instantiate(characterPrefab1, startPosition1.position, Quaternion.identity);
//            newCharacter.GetComponent<Run>().StartRunner(pathPoints1, this, 1);
//            charactersInTrack1.Add(newCharacter);

//            if (charactersInTrack1.Count == 3)
//            {
//                upgradeButton.gameObject.SetActive(true);
//            }

//            UpdateUI();
//            UpdateButtonState();
//        }
//    }
//    public void UpgradeCharacters()
//    {
//        List<List<GameObject>> tracks = new List<List<GameObject>> { charactersInTrack1, charactersInTrack2, charactersInTrack3, charactersInTrack4, charactersInTrack5, charactersInTrack6, charactersInTrack7 };
//        List<Transform[]> pathLists = new List<Transform[]> { pathPoints1, pathPoints2, pathPoints3, pathPoints4, pathPoints5, pathPoints6, pathPoints7 };
//        List<Transform> startPositions = new List<Transform> { startPosition1, startPosition2, startPosition3, startPosition4, startPosition5, startPosition6, startPosition7 };
//        List<GameObject> upgradePrefabs = new List<GameObject> { characterPrefab1, characterPrefab2, characterPrefab3, characterPrefab4, characterPrefab5, characterPrefab6, characterPrefab7 };

//        for (int i = 0; i < tracks.Count - 1; i++)
//        {
//            if (score >= upgradeCost && tracks[i].Count >= 3)
//            {
//                score -= upgradeCost;
//                upgradeCost += 25;
//                int newLevel = i + 2;
//                for (int j = 0; j < 3; j++)
//                {
//                    if (tracks[i].Count > 0)
//                    {
//                        GameObject character = tracks[i][0];
//                        character.transform.DOKill();
//                        Destroy(character);
//                        tracks[i].RemoveAt(0);
//                        Debug.Log("Sau khi nâng cấp, Track " + (i + 1) + " còn: " + tracks[i].Count + " nhân vật");
//                        Debug.Log("Track " + (i + 2) + " có: " + tracks[i + 1].Count + " nhân vật");

//                    }
//                }
//                if (i + 1 < unlockedTracks)
//                {
//                    GameObject upgradedCharacter = Instantiate(upgradePrefabs[i + 1], startPositions[i + 1].position, Quaternion.identity);
//                    upgradedCharacter.GetComponent<Run>().StartRunner(pathLists[i + 1], this, newLevel);
//                    tracks[i + 1].Add(upgradedCharacter);
//                }
//                UpdateUI();
//                UpdateButtonState();
//                return;
//            }
//        }
//    }

//    public void SizeUp()
//    {
//        if (score >= sizeUpThreshold && unlockedTracks < newTrack.Length)
//        {
//            score -= sizeUpThreshold;
//            sizeUpThreshold += sizeUpThreshold * 2;
//            newTrack[unlockedTracks].SetActive(true);
//            unlockedTracks++;
//            UpdateUI();
//            UpdateButtonState();
//        }
//    }

//    public void Income()
//    {
//        if (score >= incomeCost)
//        {
//            collectedScore += incomeCost;
//            score -= incomeCost;
//            incomeCost += 50;
//            UpdateUI();
//            UpdateButtonState();
//        }
//    }
//    void UpdateButtonState()
//    {
//        buyButton.interactable = (score >= buyCost);
//        incomeButon.interactable = (score >= incomeCost);
//        sizeUpButton.interactable = (score >= sizeUpThreshold);

//        bool canUpgrade = false;
//        bool hasEnoughCharacters = false;

//        List<List<GameObject>> tracks = new List<List<GameObject>> { charactersInTrack1, charactersInTrack2, charactersInTrack3, charactersInTrack4, charactersInTrack5, charactersInTrack6, charactersInTrack7 };

//        for (int i = 0; i < unlockedTracks - 1; i++)
//        {
//            if (tracks[i].Count >= 3)
//            {
//                hasEnoughCharacters = true;
//            }

//            if (tracks[i].Count >= 3 && score >= upgradeCost)
//            {
//                canUpgrade = true;
//                break;
//            }
//        }

//        upgradeButton.interactable = canUpgrade;
//        upgradeButton.gameObject.SetActive(hasEnoughCharacters);

//        if (unlockedTracks >= newTrack.Length)
//        {
//            sizeUpButton.gameObject.SetActive(false);
//        }
//    }


//}
