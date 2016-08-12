using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveData : MonoBehaviour {
    [Tooltip("Saved SoundVolume")]
    public float soundVolume;
    [Tooltip("Saved MusicVolume")]
    public float musicVolume;

    [Header("IsoBall Related")]
    [Tooltip("ActualSaveSlot in Use")]
    public string actualSaveSlot;
    [Tooltip("SaveState Informations")]
    public List<SaveState> saveState;
    [Tooltip("IsoBall World, Contains The Levels of the World")]
    public IsoWorld[] world;  
}

[System.Serializable]
public class IsoWorld {
    //Unity dont Serialize Multidimensional Arrays ,it need to be a Array in a Array
    [Tooltip("Levels of the World")]
    public IsoLevel[] level;
}

[System.Serializable]
public class SaveState {
    [Tooltip("SaveState Discription")]
    public string saveDiscription;
}

[System.Serializable]
public class IsoLevel {
    [Tooltip("Reference to SceneName")]
    public string sceneName;
    [Tooltip("Level is Beaten")]
    public bool isBeaten;
    [Tooltip("Actual Best Saved Time")]
    public float bestTime;
    [Tooltip("mark level as BonusLevel")]
    public bool bonus;
    [Tooltip("0.None 1.Gold 2.Silver 3.Bronce")]
    public int medal;
    [Tooltip("Actual last Saved CamDirection")]
    public int actualCamDirection;
}



