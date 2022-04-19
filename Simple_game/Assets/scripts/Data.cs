using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="data")]
public class Data : ScriptableObject{
    public int score;
    public int life;
    public bool is_play = false;
}
