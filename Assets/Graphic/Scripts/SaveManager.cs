using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string PlayerKey => "PlayerData";
    private string SceneKey => $"SceneData_Map_{Map.Instance.currentMapIndex}";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);


    }

    public void SaveGame()
    {
        SavePlayerData();
        SaveSceneData();
        Debug.Log("Game saved to PlayerPrefs");
    }

    public void LoadGameData()
    {
        LoadPlayerData();
        LoadSceneData();
        Debug.Log("Game loaded from PlayerPrefs");
    }

    public void DeleteAllSaveData()
    {
        PlayerPrefs.DeleteKey(PlayerKey);
        PlayerPrefs.DeleteKey(SceneKey);
        Debug.Log("All PlayerPrefs data deleted");
    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData
        {
            collectedScore = GameManager.Instance.collectedScore,
            nextTrackCost = GameManager.Instance.nextTrackCost,
            prevTrackCost = GameManager.Instance.prevTrackCost,
            currentMapIndex = Map.Instance.currentMapIndex

        };

        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PlayerKey, json);
    }

    public void LoadPlayerData()
    {
        if (!PlayerPrefs.HasKey(PlayerKey)) return;

        string json = PlayerPrefs.GetString(PlayerKey);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        GameManager.Instance.collectedScore = data.collectedScore;
        GameManager.Instance.nextTrackCost = data.nextTrackCost;
        GameManager.Instance.prevTrackCost = data.prevTrackCost;
        Map.Instance.currentMapIndex = data.currentMapIndex;
    }
    public void CharacterDataFromTrack(int trackIndex, SceneData sceneData)
    {
        var trackList = CharacterManager.Instance.GetTrack(trackIndex);

        for (int j = 0; j < trackList.Count; j++)
        {
            var runner = trackList[j];
            var rn = runner.GetComponent<Runner>();
            var pos = runner.transform.position;

            sceneData.characters.Add(new CharacterSaveData
            {
                trackIndex = trackIndex,
                characterLevel = rn.level,
                posX = pos.x,
                posY = pos.y,
                posZ = pos.z,
                normalizedTime = rn.splineAnimate.NormalizedTime
            });
        }
    }

    private void SaveSceneData()
    {
        SceneData sceneData = new SceneData
        {
            score = GameManager.Instance.score,
            buyCost = GameManager.Instance.buyCost,
            upgradeCost = GameManager.Instance.upgradeCost,
            incomeCost = GameManager.Instance.incomeCost,
            sizeUpThreshold = GameManager.Instance.sizeUpThreshold,
            unlockedTracks = Map.Instance.CurrentMapInstance.GetComponent<MapData>().unlockedTracks
        };

        for (int i = 0; i < CharacterManager.Instance.TrackCount; i++)
        {
           CharacterDataFromTrack(i, sceneData);
        }

        string json = JsonUtility.ToJson(sceneData);
        PlayerPrefs.SetString(SceneKey, json);
    }
  

    public void LoadSceneData()
    {
        if (!PlayerPrefs.HasKey(SceneKey)) return;

        string json = PlayerPrefs.GetString(SceneKey);
        SceneData data = JsonUtility.FromJson<SceneData>(json);

        GameManager.Instance.score = data.score;
        GameManager.Instance.buyCost = data.buyCost;
        GameManager.Instance.upgradeCost = data.upgradeCost;
        GameManager.Instance.incomeCost = data.incomeCost;
        GameManager.Instance.sizeUpThreshold = data.sizeUpThreshold;

        var map = Map.Instance.CurrentMapInstance.GetComponent<MapData>();
        map.unlockedTracks = data.unlockedTracks;
        map.ShowTracks();

        CharacterManager.Instance.ClearAllCharacters();

        for (int i = 0; i < data.characters.Count; i++)
        {
            var person = data.characters[i];
            GameObject prefab = CharacterManager.Instance.characterPrefabs[person.characterLevel - 1];
            Vector3 pos = new Vector3(person.posX, person.posY, person.posZ);
            GameObject character = Instantiate(prefab, pos, Quaternion.identity);

            Runner runner = character.GetComponent<Runner>();
            runner.StartRunner(map.splines[person.trackIndex],person.characterLevel);

            var anim = runner.splineAnimate;
            anim.Pause();
            anim.NormalizedTime = person.normalizedTime;
            anim.Play();

            CharacterManager.Instance.AddToTrack(person.trackIndex, character);
        }
    }
}
