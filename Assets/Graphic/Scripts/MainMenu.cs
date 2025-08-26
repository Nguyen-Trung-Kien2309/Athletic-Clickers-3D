using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    private string selectedScene = "";

    public Color selectedColor = Color.red;
    public Color defaultColor = new Color(1, 1, 1, 0);
    public Color lockedColor = new Color(0.3f, 0.3f, 0.3f, 0.7f); 

    [System.Serializable]
    public class MapButton
    {
        public string sceneName;
        public int sceneBuildIndex;
        public Outline outline;
        public Button button;
        public Image buttonImage; 
    }

    public List<MapButton> mapButtons;

    void Start()
    {
        //PlayerPrefs.DeleteAll(); 
        //PlayerPrefs.Save();
        if (!PlayerPrefs.HasKey("UnlockedMap_1"))
        {
            PlayerPrefs.SetInt("UnlockedMap_1", 1);
            PlayerPrefs.Save();
        }

        UpdateMapButtons();
        SelectField("Scene1"); 
    }

    void UpdateMapButtons()
    {
        foreach (var map in mapButtons)
        {
            bool isUnlocked = PlayerPrefs.GetInt("UnlockedMap_" + map.sceneBuildIndex, 0) == 1;

            map.button.interactable = isUnlocked;

            if (map.buttonImage != null)
            {
                map.buttonImage.color = isUnlocked ? Color.white : lockedColor;
            }

            if (map.outline != null)
            {
                map.outline.effectColor = isUnlocked ? defaultColor : Color.clear;
            }
        }
    }

    public void SelectField(string sceneName)
    {
        var selectedMap = mapButtons.Find(m => m.sceneName == sceneName);
        if (selectedMap == null || PlayerPrefs.GetInt("UnlockedMap_" + selectedMap.sceneBuildIndex, 0) != 1)
        {
            return; 
        }

        selectedScene = sceneName;
        Debug.Log("Selected Scene: " + selectedScene);

        foreach (var map in mapButtons)
        {
            if (map.outline != null)
            {
                bool isUnlocked = PlayerPrefs.GetInt("UnlockedMap_" + map.sceneBuildIndex, 0) == 1;
                if (isUnlocked)
                {
                    map.outline.effectColor = (map.sceneName == sceneName) ? selectedColor : defaultColor;
                }
            }
        }
    }

    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            SceneManager.LoadScene(selectedScene);
        }
        else
        {
            SceneManager.LoadScene("Scene1");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}