using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateToSplash : MonoBehaviour {



	// Use this for initialization
	public void OnClick () {
		//should return to main menu when implemented
		SceneManager.LoadScene(0);
	}
}
