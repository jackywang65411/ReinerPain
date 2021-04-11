using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScene : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		Invoke("Quit", 8f);
	}

	public void Quit() {
		Application.Quit();
	}
}