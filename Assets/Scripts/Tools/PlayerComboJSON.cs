using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class PlayerComboJSON : MonoBehaviour
{
    // COMBO DATA
    private string comboInput;
    private JSONArray hitboxes;
    // optional add-on function to be called, HAVEN'T ADDED THIS YET
    public string function;

    // HITBOX DATA
    // private string hitboxName;
    private Vector3 position;    // change to Vector3 if necessary
    // times are in millis
    private int startup;
    private int active;
    private int endlag;
    private int damage;

    // tables loaded at the start
    private JSONObject comboTable;
    private JSONObject hitboxTable;

    // use for setting table format
    void writeJSON() {
        comboTable = new JSONObject();
        JSONArray hitboxNames = new JSONArray();
        hitboxNames.Add("boxA");
        hitboxNames.Add("boxB");
        hitboxNames.Add("boxC");
        comboTable.Add("ABA", hitboxNames);
        string path = Application.dataPath + "/Scripts/Tools/PlayerCombos.json";
        File.WriteAllText(path, comboTable.ToString());
        Debug.Log(path);

        hitboxTable = new JSONObject();
        JSONObject hitboxAData = new JSONObject();
        position = new Vector3(0, 0, 0);
        hitboxAData.Add("position", position);
        startup = 4;
        hitboxAData.Add("startup", startup);
        active = 6;
        hitboxAData.Add("active", active);
        endlag = 2;
        hitboxAData.Add("endlag", endlag);
        damage = 5;
        hitboxAData.Add("damage", damage);
        hitboxTable.Add("boxA", hitboxAData);
        JSONObject hitboxBData = new JSONObject();
        position = new Vector3(1, 3, 0);
        hitboxBData.Add("position", position);
        startup = 4;
        hitboxBData.Add("startup", startup);
        active = 6;
        hitboxBData.Add("active", active);
        endlag = 2;
        hitboxBData.Add("endlag", endlag);
        damage = 5;
        hitboxBData.Add("damage", damage);
        hitboxTable.Add("boxB", hitboxBData);
        JSONObject hitboxCData = new JSONObject();
        position = new Vector3(2, 5, 0);
        hitboxCData.Add("position", position);
        startup = 4;
        hitboxCData.Add("startup", startup);
        active = 6;
        hitboxCData.Add("active", active);
        endlag = 2;
        hitboxCData.Add("edglag", endlag);
        damage = 5;
        hitboxCData.Add("damage", damage);
        hitboxTable.Add("boxC", hitboxCData);

        
        path = Application.dataPath + "/Scripts/Tools/Hitboxes.json";
        File.WriteAllText(path, hitboxTable.ToString());
        Debug.Log(path);
    }
    // load all JSON tables at the start
    void loadJSON() {
        string path = Application.dataPath + "/Scripts/Tools/PlayerCombos.json";
        string jsonString = File.ReadAllText(path);
        comboTable = (JSONObject)JSON.Parse(jsonString);
        Debug.Log(comboTable);
        path = Application.dataPath + "/Scripts/Tools/Hitboxes.json";
        jsonString = File.ReadAllText(path);
        hitboxTable = (JSONObject)JSON.Parse(jsonString);
        Debug.Log(hitboxTable);
    }

    // returns JSONArray of hitbox names, use JSONArray as you would a normal array
    JSONArray getHitboxes(string comboInput) {
        return comboTable[comboInput].AsArray;
    }

    Vector3 getPosition(string hitboxName) {
        return hitboxTable[hitboxName]["position"].ReadVector3();
    }

    int getStartup(string hitboxName) {
        return hitboxTable[hitboxName]["startup"];
    }

    int getActive(string hitboxName) {
        return hitboxTable[hitboxName]["active"];
    }

    int getEndlag(string hitboxName) {
        return hitboxTable[hitboxName]["endlag"];
    }

    int getDamage(string hitboxName) {
        return hitboxTable[hitboxName]["damage"];
    }

    void Start() {
        loadJSON();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            // writeJSON();
            JSONArray boxes = getHitboxes("ABA");
            Debug.Log(boxes);
            Debug.Log(getPosition("boxA"));
            Debug.Log(getEndlag("boxA"));
        }
        // if (Input.GetKeyDown(KeyCode.L)) Load();
    }

}
