using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;
    public bool resetSave;
    public float saveTime = 1;

    private void Awake(){
        if(resetSave)
            ResetSave();

        if (Instance == null)
            Instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Load();
        StartCoroutine(ConstantSave());
        GameObject.FindObjectOfType<SceneLoader>().LoadScene(1);
    }

    IEnumerator ConstantSave(){
        while(true){
            yield return new WaitForSeconds(saveTime);
            Save();
        }
    }

    public void Save(){
        PlayerPrefs.SetString("save", SaveUtils.Serialize<SaveState>(state));
    }

    public void Load(){
        if (PlayerPrefs.HasKey("save")){
            state = SaveUtils.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            Debug.Log("Save file loaded!");
        }
        else{
            state = new SaveState();
            Save();
            Debug.Log("No save file... New one created.");
        }
    }

    public void ResetSave(){
        PlayerPrefs.DeleteKey("save");
        Debug.Log("Save Reseted");
        Load();
    }
}