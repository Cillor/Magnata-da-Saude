using System; 
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {
	[TextArea(5, 10)]
	public string sentence;
    public Button interactable;
	public UDateTime timeSet;
}
