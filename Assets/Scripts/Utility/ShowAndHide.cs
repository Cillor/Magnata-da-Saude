using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour{
    public void ToggleGameObject(){
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
