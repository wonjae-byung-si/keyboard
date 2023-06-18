using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour{

	public void LoadScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}

	public void LoadScene(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}

	public void LoadSceneAsync(string sceneName){
		SceneManager.LoadSceneAsync(sceneName);
	}

	public void LoadSceneAsync(int sceneIndex){
		SceneManager.LoadSceneAsync(sceneIndex);
	}

	public void Quit(){
		Debug.Log("Quit lmao");
		Application.Quit();
	}
}
