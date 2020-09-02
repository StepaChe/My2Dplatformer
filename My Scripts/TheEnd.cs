using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{
	void Start ()
    {
        Invoke("LoadScene", 70);
	}

    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
