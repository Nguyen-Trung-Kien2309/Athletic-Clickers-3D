
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    private SplineContainer[] currentSplines;
    private Transform[] currentStartPositions;
    public GameObject[] characterPrefabs;
    private List<List<GameObject>> characters = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetSpline(SplineContainer[] splines, Transform[] starts)
    {
        currentSplines = splines;
        currentStartPositions = starts;
        ClearAllCharacters();
        characters.Clear();
        for (int i = 0; i < splines.Length; i++)
            characters.Add(new List<GameObject>());
}


    public void SpawnRunner()
    {
        if (currentStartPositions == null || currentSplines == null) return;

        GameObject runner = Instantiate(characterPrefabs[0], currentStartPositions[0].position, Quaternion.identity);
        runner.GetComponent<Runner>().StartRunner(currentSplines[0], 1);
        characters[0].Add(runner);
    }




    public bool CanUpgrade()
    {
        MapData mapData = Map.Instance.CurrentMapInstance?.GetComponent<MapData>();
        if (mapData == null) return false;

        for (int i = 0; i < mapData.unlockedTracks - 1; i++)
        {
            if (characters[i].Count >= 3)
                return true;
        }
        return false;
    }
    public int TrackCount => characters.Count;
    public List<GameObject> GetTrack(int index) => characters[index];
    public void AddToTrack(int trackIndex, GameObject obj) => characters[trackIndex].Add(obj);
    public void ClearAllCharacters()
    {
        foreach (var track in characters)
        {
            foreach (var obj in track) Destroy(obj);
            track.Clear();
        }
    }

    public void UpgradeRunner()
    {
        MapData mapData = Map.Instance.CurrentMapInstance?.GetComponent<MapData>();
        if (mapData == null) return;

        for (int i = 0; i < mapData.unlockedTracks - 1; i++)
        {
            if (characters[i].Count >= 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject go = characters[i][0];
                    characters[i].RemoveAt(0);
                    Destroy(go);
                }

                GameObject upgraded = Instantiate(characterPrefabs[i + 1], currentStartPositions[i + 1].position, Quaternion.identity);
                upgraded.GetComponent<Runner>().StartRunner(currentSplines[i + 1], i + 2);
                characters[i + 1].Add(upgraded);

                AudioManager.Instance.PlayUpgradeSound();
                UIManager.Instance.UpdateUI();
                UIManager.Instance.UpdateButtonState();
                break;
            }
        }
    }


}
