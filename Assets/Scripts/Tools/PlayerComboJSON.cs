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
    private string function;

    // HITBOX DATA
    // private string hitboxName;
    private Vector3 position;    // change to Vector3 if necessary
    // times are in millis
    private int startup;
    private int active;
    private int endlag;
    private int damage;

    // tables loaded at the start
    // private JSONObject comboTable;
    private JSONObject hitboxTable;

    // load all JSON tables at the start
    void loadJSON() {
        //Debug.Log(comboTable);
        string path = Application.dataPath + "/Scripts/Tools/Hitboxes.json";
        string jsonString = File.ReadAllText(path);
        hitboxTable = (JSONObject)JSON.Parse(jsonString);
        //Debug.Log(hitboxTable);
    }

    // public JSONArray getHitboxes(string comboInput) {
    //     return comboTable[comboInput].AsArray;
    // }

    public Vector3 getPosition(string hitboxName) {
        return hitboxTable[hitboxName]["position"].ReadVector3();
    }

    public int getStartup(string hitboxName) {
        return hitboxTable[hitboxName]["startup"];
    }

    public int getActive(string hitboxName) {
        return hitboxTable[hitboxName]["active"];
    }

    public int getEndlag(string hitboxName) {
        return hitboxTable[hitboxName]["endlag"];
    }

    public int getDamage(string hitboxName) {
        return hitboxTable[hitboxName]["damage"];
    }

    void Start() {
        loadJSON();
    }



}
