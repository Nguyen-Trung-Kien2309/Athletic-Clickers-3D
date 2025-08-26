using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int collectedScore;
    public int nextTrackCost;
    public int prevTrackCost;
    public int currentMapIndex;
}

[System.Serializable]
public class SceneData
{
    public int score;
    public int buyCost;
    public int upgradeCost;
    public int incomeCost;
    public int sizeUpThreshold;
    public int unlockedTracks;
    public List<CharacterSaveData> characters = new();
}

[System.Serializable]
public class CharacterSaveData
{
    public int trackIndex;
    public int characterLevel;
    public float posX, posY, posZ;
    public float normalizedTime;
}
