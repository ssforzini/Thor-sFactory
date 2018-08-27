using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	private bool loadScene = false;

	[SerializeField]
	private int scene;
	private GameObject loadingText;
	private MainMenu wc;
	private int alreadyLoad;

	void Start(){
		alreadyLoad = 0;
		loadingText = GameObject.Find ("Loading");
		if(loadingText != null){
			loadingText.SetActive (false);	
		}
		wc = GameObject.Find ("MainMenuManager").GetComponent<MainMenu> ();
	}

	// Updates once per frame
	void Update() {
		if (loadScene == false && wc.selectedLevel != 0) {
			loadScene = true;
			if(loadingText != null){
				loadingText.SetActive(true);
			}
			StartCoroutine(LoadNewScene());
		}
	}

	IEnumerator LoadNewScene() {
		
		if(SceneManager.GetActiveScene().name == "Main Menu" && alreadyLoad == 0){
			alreadyLoad = 1;
			AsyncOperation async = null;
			string levelToLoad = "Level" + wc.selectedLevel;
			Application.LoadLevelAsync (levelToLoad);
			loadScene = false;
			// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
			while (!async.isDone) {
				yield return null;
			}
		}
	}
}
