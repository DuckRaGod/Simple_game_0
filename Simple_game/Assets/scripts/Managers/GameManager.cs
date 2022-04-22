using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    //  Bounds of box to spawn targets!
    public float x;
    public float p_y; 
    public int n_y;
    public float effect_spawn_pos_y;

    //  Game data for target!
    //  How many targets hit seccsfuly!
    int xp = 0; 
    //  Level of target to spawn!
    int level = 0;
    //  After reaching max level will boost speed of targets!
    float max_level_speed_boost = 0;

    readonly List<GameObject> targets = new();

    public Data data;
    public UiManager ui_manager;

    //  In play mode ui!
    public GameObject ui_canves;
    //  Start menu ui!
    public GameObject main_canves;   
    //  Death ui with retry score and highscore!
    public GameObject death_canves;  
    public GameObject player;
    
    //  Effect prefabs!
    public GameObject[] effect_prefab;
    int effect_arr_length;
    //  Time to spawn effect!
    public float timer_effect;
    float time_effect;

    int amount_of_levels;
    
    public static GameManager Instance { get; private set; }

    void Awake(){
        //  Set game data!
        data.is_play = false;
        data.isPause = false;
        data.life = 3;
        data.score = 0;
        data.highest_score = 0;

        amount_of_levels = data.target_bank.target_level.Length;
        effect_arr_length = effect_prefab.Length;

        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }
    
    IEnumerator Set_play(){
        yield return new WaitForSeconds(0.1f);
        data.is_play = true;
    }

    void SpawnEffect(){
        for (int i = 0; i < effect_arr_length; i++){
            if(effect_prefab[i].activeSelf == true) return;
        }
        if(Random.Range(0f,1f) < 0.5f) return;

        var j = Random.Range(0,effect_arr_length);
        var effect = effect_prefab[j];
        effect.transform.position = new Vector2(Random.Range(-x,x),effect_spawn_pos_y);
        if(j == 0){
            var pos = targets[0].transform.position - effect.transform.position;

            var _angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            effect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        }
        effect.SetActive(true);
    }

    public void ReSpawn(GameObject game_object){
        print("A");
        game_object.transform.position  = new Vector2(Random.Range(-x,x),Random.Range(n_y,p_y));
    }

    void Update(){
        //  Todo change to exit to menu ui with shop maybe?
        //  Quit game!
        if(Input.GetKeyDown("escape")){
            Application.Quit();
        }

        if(data.is_play){
            if(timer_effect <= 0) return;
            time_effect += Time.deltaTime;
            if(time_effect >= timer_effect){
                SpawnEffect();
                time_effect = 0;
            }
            return;
        }
        if(data.isPause){
            if(!data.is_play) data.isPause = false;
            return;
        }

        if(Input.anyKeyDown){
            JuiceManager.Instance.Enter();
            ui_canves.SetActive(true);
            main_canves.SetActive(false);
            player.SetActive(true);
            death_canves.SetActive(false);
            var obj = Instantiate(data.target_bank.target_level[0].target_prefab[0] , Vector3.zero , Quaternion.identity);//Create Target at start of the game!
            targets.Add(obj);
            StartCoroutine(Set_play());   
            ui_manager.Update_ui();
        }
    }

    //  Spawn target randomly at box bounds!
    public void Place_target(){
        if(targets.Count > 0) return;
        var number = Random.Range(0,data.target_bank.target_level[level].target_prefab.Length);
        var target = data.target_bank.target_level[level].target_prefab[number];
        var rot = Quaternion.Euler(0,0,Random.Range(0,360f));
        var pos = new Vector2(Random.Range(-x,x),Random.Range(n_y,p_y));
        var obj = Instantiate(target,pos,rot);
        targets.Add(obj);
        obj.transform.GetChild(0).GetComponent<Target>().speed += max_level_speed_boost;
    }

    public void Hit_target(GameObject game_object){
        if(targets.Count <= 0) return;
        targets.Remove(game_object);
        Destroy(game_object);
        xp++;
        data.score ++;
        ui_manager.Update_ui();

        //  Check index out of bounds!
        if(level + 1 < amount_of_levels){
            //  Check if xp is enough !
            if(xp == data.target_bank.target_level[level + 1].xp_needed){
                xp = 0;
                level++;
            }
        }else{ 
            max_level_speed_boost += Random.Range(.25f,.5f);
        }
        Place_target();
    }

    public void Hit_obstacle(GameObject game_object){
        if(targets.Count <= 0) return;
        targets.Remove(game_object);
        Destroy(game_object);
        data.life--;
        ui_manager.Update_ui();
        if(data.life <= 0){
            Death();
            return;
        }
        Place_target();
    }

    void Death(){
        JuiceManager.Instance.PlayerDie();
        data.life = 3;
        player.SetActive(false);
        //  Check if score is higher then highestscore that stored if yes then replace highestscore with score!
        if(data.score > data.highest_score){
            data.highest_score = data.score;
        }
        //  Update highestscore and score. needed to be before reseting score.
        ui_manager.Update_ui_death();

        //  Reseting data!
        data.score = 0;
        level = 0;
        xp = 0;
        data.is_play = false;

        ui_canves.SetActive(false);
        death_canves.SetActive(true);
    }
}
