using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonBehaviour : MonoBehaviour {

	public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
