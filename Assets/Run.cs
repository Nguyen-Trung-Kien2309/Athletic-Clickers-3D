////using DG.Tweening;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.SocialPlatforms.Impl;
//using UnityEngine.Splines;

//public class Run : MonoBehaviour
//{
//    public float duration = 5f;
//    private float originalDuration;
//    private Game1 gameManager;
//    private Tweener tweener;
//    public int level = 1;

//    private void Start()
//    {
//        originalDuration = duration;
//    }

//    public void StartRunner(Transform[] pathPoints, Game1 manager, int characterLevel)
//    {
//        gameManager = manager;
//        level = characterLevel;
//        Vector3[] waypoints = new Vector3[pathPoints.Length];
//        for (int i = 0; i < pathPoints.Length; i++)
//        {
//            waypoints[i] = pathPoints[i].position;
//        }

//        tweener = transform.DOPath(waypoints, duration, PathType.CatmullRom)
//            .SetEase(Ease.Linear)
//            .SetLoops(-1, LoopType.Restart)
//            .SetLookAt(0.01f);
//    }

//    public void SetSpeedMultiplier(float multiplier)
//    {
//        if (tweener != null)
//        {
//            tweener.timeScale = multiplier;
//        }
//    }

//    //private void OnTriggerEnter(Collider other)
//    //{
//    //    if (other.CompareTag("FinishLine"))
//    //    {
//    //        gameManager.AddScore(level);
//    //    }
//    //}
//}

////public void SaveGameData()
////{
////    GameData data;
////    if (PlayerPrefs.HasKey("GameData"))
////    {
////        string savedJson = PlayerPrefs.GetString("GameData");
////        data = JsonUtility.FromJson<GameData>(savedJson);
////    }
////    else
////    {
////        data = new GameData();
////    }

////    string currentSceneName = SceneManager.GetActiveScene().name;

////    SceneData currentSceneData = new SceneData
////    {
////        sceneName = currentSceneName,
////        score = score,
////        buyCost = buyCost,
////        upgradeCost = upgradeCost,
////        incomeCost = incomeCost,
////        sizeUpThreshold = sizeUpThreshold,
////        unlockedTracks = unlockedTracks,
////        characters = new List<CharacterData>()
////    };
////    List<List<GameObject>> tracks = new List<List<GameObject>>
////    {charactersInTrack1, charactersInTrack2, charactersInTrack3, charactersInTrack4, charactersInTrack5, charactersInTrack6, charactersInTrack7
////    };

////    for (int i = 0; i < tracks.Count; i++)
////    {
////        foreach (var character in tracks[i])
////        {
////            if (character != null)
////            {
////                int characterLevel = character.GetComponent<Runner>().level;
////                currentSceneData.characters.Add(new CharacterData { trackIndex = i, characterLevel = characterLevel });
////            }
////        }
////    }
////    data.collectedScore = collectedScore;
////    data.nextSceneCost = nextSceneCost;
////    data.prevSceneCost = prevSceneCost;
////    data.SetSceneData(currentSceneData);

////    string json = JsonUtility.ToJson(data);
////    PlayerPrefs.SetString("GameData", json);
////    PlayerPrefs.Save();
////}

////void LoadGameData()
////{
////    if (!PlayerPrefs.HasKey("GameData")) return;

////    string json = PlayerPrefs.GetString("GameData");
////    GameData data = JsonUtility.FromJson<GameData>(json);

////    collectedScore = data.collectedScore;
////    nextSceneCost = data.nextSceneCost;
////    prevSceneCost = data.prevSceneCost;
////    string currentSceneName = SceneManager.GetActiveScene().name;
////    SceneData currentSceneData = data.GetSceneData(currentSceneName);

////    if (currentSceneData != null)
////    {
////        score = currentSceneData.score;
////        buyCost = currentSceneData.buyCost;
////        upgradeCost = currentSceneData.upgradeCost;
////        incomeCost = currentSceneData.incomeCost;
////        sizeUpThreshold = currentSceneData.sizeUpThreshold;
////        unlockedTracks = currentSceneData.unlockedTracks;

////        for (int i = 0; i < unlockedTracks; i++)
////        {
////            newTrack[i].SetActive(true);
////        }

////        List<List<GameObject>> tracks = new List<List<GameObject>> { charactersInTrack1, charactersInTrack2, charactersInTrack3, charactersInTrack4, charactersInTrack5, charactersInTrack6, charactersInTrack7 };
////        List<GameObject> characterPrefabs = new List<GameObject> { characterPrefab1, characterPrefab2, characterPrefab3, characterPrefab4, characterPrefab5, characterPrefab6, characterPrefab7 };
////        List<SplineContainer> pathLists = new List<SplineContainer> { trackSplines[0], trackSplines[1], trackSplines[2], trackSplines[3], trackSplines[4], trackSplines[5], trackSplines[6] };
////        List<Transform> startPositions = new List<Transform> { startPosition1, startPosition2, startPosition3, startPosition4, startPosition5, startPosition6, startPosition7 };
////        foreach (var charData in currentSceneData.characters)
////        {
////            if (charData.characterLevel > 0 && charData.characterLevel <= characterPrefabs.Count)
////            {
////                GameObject character = Instantiate(characterPrefabs[charData.characterLevel - 1], startPositions[charData.trackIndex].position, Quaternion.identity);
////                character.GetComponent<Runner>().StartRunner(pathLists[charData.trackIndex], this, charData.characterLevel);
////                tracks[charData.trackIndex].Add(character);
////            }
////        }
////    }
////}