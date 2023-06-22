using System.Collections.Generic;

[System.Serializable]
public class GameSaveObject 
{
    public PlayerSave Player = new PlayerSave();
    public List<PlayerSave> Players = new List<PlayerSave>();
    public Dictionary<string, CheckPointSave> Checkpoints = new Dictionary<string, CheckPointSave>();
}
