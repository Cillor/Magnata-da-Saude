using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour{
    public static int[] date = new int[6];
    
    public static PlayerData PD;
     
     void Awake()
     {
         if(PD != null)
             GameObject.Destroy(PD);
         else
             PD = this;
         
         DontDestroyOnLoad(this);
     }
}