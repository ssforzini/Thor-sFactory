using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	private int alreadyLoad;
	private bool loadScene = false;

	//[SerializeField] private GameObject loadingWindow;
	[SerializeField] private LevelManager lm;

	void Start(){
		alreadyLoad = 0;
		//loadingWindow.SetActive (false);
	}

	// Updates once per frame
	void Update() {
		if (loadScene == false) {
			if(lm.checkLevelWinConfirmation ()){
				//loadingWindow.SetActive (true);
				loadScene = true;
				StartCoroutine (LoadNewScene (1));
			} else if(lm.exitActivate) {
				loadScene = true;
				StartCoroutine (LoadNewScene (2));
			}
		}
	}

	IEnumerator LoadNewScene(int type) {
		if (alreadyLoad == 0) {
			string levelToLoad = "";
			if (type == 1) {
				int actualLevel;
				int.TryParse (SceneManager.GetActiveScene ().name.ToString ().Substring (5), out actualLevel);
				actualLevel++;
				levelToLoad = "Level" + actualLevel;
			} else {
				levelToLoad = "MainMenu";
			}
			alreadyLoad = 1;
			AsyncOperation async = null;
			async = SceneManager.LoadSceneAsync (levelToLoad);
			loadScene = false;
			// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
			while (!async.isDone) {
				yield return null;
			}
		}
	}
}
