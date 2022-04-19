using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour{
    public TextMeshProUGUI Lives_text;
    public TextMeshProUGUI Score_text;
    public Data data;
    
    public void Update_ui(){
        Lives_text.text = $"Lives: {data.life}";
        Score_text.text = $"Score: {data.score}";
    }
}
