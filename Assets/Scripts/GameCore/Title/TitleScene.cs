using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
	public Button btnStart;
	public GameObject objCutin;
	public AudioSource audbgm;
    private void Start() {
		btnStart.interactable = true;
		objCutin.SetActive(false);

	}
    public void QuitGame() {
		btnStart.interactable = false;
		StartCoroutine(StartGame());
	}
	IEnumerator StartGame() {
		audbgm.Stop();
		objCutin.SetActive(true);
		AudioManagerScript.Instance.PlayAudioClip("reiner_seat_down");
		yield return new WaitForSeconds(1.8f);
		SceneManager.LoadScene("GameScene");
	}
}
