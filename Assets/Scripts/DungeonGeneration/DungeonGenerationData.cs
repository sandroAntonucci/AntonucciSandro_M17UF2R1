using UnityEngine;

// Scriptable object


[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]


public class DungeonGenerationData : ScriptableObject
{

    public int numberOfCrawlers;

    public int iterationMin;

    public int iterationMax;


}
