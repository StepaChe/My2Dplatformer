using UnityEngine;

public class InformScripts : MonoBehaviour
{
    [SerializeField] private GameObject _info;

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        _info.SetActive(false);
        Time.timeScale = 1;
    }
    
}
