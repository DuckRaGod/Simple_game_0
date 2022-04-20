using UnityEngine;

[CreateAssetMenu(menuName ="data")]
public class Data : ScriptableObject{
    public int highest_score;
    public int score;
    public int life;
    public bool is_play = false;

    public TargetBank target_bank;  //  bank of levels -> bank of prefabs
}

[System.Serializable]
public class TargetBank{
    public Level[] target_level;
}

[System.Serializable]
public class Level{
    public int xp_needed;
    public GameObject[] target_prefab;
}
