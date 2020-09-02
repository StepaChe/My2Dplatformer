using UnityEngine;

public class Fake : MonoBehaviour
{
    [SerializeField] private Animator _myAnim;

    private void Start()
    {
        _myAnim = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _myAnim.SetTrigger("Enter");
    }
}
