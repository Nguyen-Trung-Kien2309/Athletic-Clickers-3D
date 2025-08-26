using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;

    [SerializeField] public GameObject[] mapPrefabs;
    private GameObject currentMapInstance;
    public Transform firePoint;
    public int currentMapIndex = 0;

    public GameObject CurrentMapInstance => currentMapInstance; 

      void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

   public void map()
    {
        LoadMap(currentMapIndex); 
        //SaveManager.Instance.LoadGameData();
    }

    public void LoadMap(int index)
    {
        if (currentMapInstance != null)
            Destroy(currentMapInstance);

        currentMapInstance = Instantiate(mapPrefabs[index], firePoint.position, firePoint.rotation);
        currentMapIndex = index;

        MapData map = currentMapInstance.GetComponent<MapData>();
        map.ShowTracks(); 
        CharacterManager.Instance.SetSpline(map.splines, map.startPositions);
        CharacterManager.Instance.SpawnRunner();
    }
    public void SwitchToNextMap()
    {
        if (currentMapIndex < mapPrefabs.Length - 1)
        {
            int cost = GameManager.Instance.nextTrackCost;
            if (GameManager.Instance.score >= cost)
            {  
                GameManager.Instance.score -= cost;
                GameManager.Instance.nextTrackCost += cost / 2;
                SaveManager.Instance.SaveGame();
                LoadMap(currentMapIndex + 1);
                SaveManager.Instance.SavePlayerData();
                SaveManager.Instance.LoadGameData();
                //SaveManager.Instance.DeleteAllSaveData();

                AudioManager.Instance.PlayUpgradeSound();
                UIManager.Instance.UpdateUI();
            }
        }
    }
    public void SwitchToPreviousMap()
    {
        if (currentMapIndex > 0)
        {
            int cost = GameManager.Instance.prevTrackCost;
            if (GameManager.Instance.score >= cost)
            {  
             
                GameManager.Instance.score -= cost;
                GameManager.Instance.prevTrackCost += cost / 2;
                SaveManager.Instance.SaveGame();

                LoadMap(currentMapIndex - 1);
                SaveManager.Instance.SavePlayerData();
                SaveManager.Instance.LoadGameData();
                //SaveManager.Instance.DeleteAllSaveData();

                AudioManager.Instance.PlayUpgradeSound();
                UIManager.Instance.UpdateUI();
            }
        }
    }

}
