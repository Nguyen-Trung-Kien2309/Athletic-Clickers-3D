using UnityEngine;
using UnityEngine.Splines;

public class MapData : MonoBehaviour
{
    public SplineContainer[] splines;
    public Transform[] startPositions;
    public GameObject[] trackObjects;

    public int unlockedTracks = 2;

    public void ShowTracks()
    {
        for (int i = 0; i < trackObjects.Length; i++)
        {
            trackObjects[i].SetActive(i < unlockedTracks);
        }
    }

    public void UnlockTrack()
    {
        if (unlockedTracks >= trackObjects.Length) return;
        unlockedTracks++;
        ShowTracks();
    }
}

