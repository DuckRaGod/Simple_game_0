using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour{
    public TextMeshProUGUI Lives_text;
    public TextMeshProUGUI Score_text;

    public TextMeshProUGUI Death_score_text;
    public TextMeshProUGUI Highest_score_text;
    public Data data;
    
    public void Update_ui(){
        Lives_text.text = $"Lives: {data.life}";
        Score_text.text = $"Score: {data.score}";
    }

    public void Update_ui_death(){
        Death_score_text.text = "Score: " + data.score.ToString();
        Highest_score_text.text = "Highest score: " + data.highest_score.ToString();
    }
}
