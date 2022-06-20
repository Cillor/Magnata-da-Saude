using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public TMP_Text dialogueText;
    public GameObject dialogueGO;


	private Queue<string> sentences;

	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue){
        dialogueGO.SetActive(true);
		sentences.Clear();

		foreach (string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence (){
		if (sentences.Count == 0){
            SaveManager.Instance.state.tutorialCompleted = true;
            dialogueGO.SetActive(false);
			return;
        }

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()){
			dialogueText.text += letter;
			yield return null;
		}
	}

}