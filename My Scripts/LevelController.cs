using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] protected bool _key;
    [SerializeField] private GameObject _next;
    //[SerializeField] private GameObject _hero;
    //[SerializeField] private GameObject _boss;

    private bool _pause = false;
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
       // if (_boss.IsActive()) 
	}

    public void Pause()
    {
        _menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {        
        _menu.SetActive(false);
        Time.timeScale = 1;        
    }

    public void LoadScene    (int num)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(num);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public virtual void Next()
    {
        _next.SetActive(true);
        Time.timeScale = 0;
    }

    public virtual void Stay()
    {
        _next.SetActive(false);
        Time.timeScale = 1;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _key = collision.gameObject.GetComponent<Player_Controller>().Key;

            if (_key == true)
            {
                Next();
            }
        }
    }
}
