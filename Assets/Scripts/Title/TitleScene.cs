﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void QuitGame() {
		SceneManager.LoadScene("Quit");
	}
}
