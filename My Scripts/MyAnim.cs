 using UnityEngine;

public class MyAnim : MonoBehaviour
{
    [SerializeField] public GameObject _bazooka;
    public Animator MyAnimCtrl;
    private bool _isMove;
    private bool _jump = true;
    private bool _cd1;
    private bool _cd2;

    private Rigidbody2D _myRg;
    

	// Use this for initialization
	void Start ()
    {
        MyAnimCtrl = GetComponent<Animator>();

        _myRg = GetComponentInParent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Получаем ссылку на логическую переменную родительского объекта каждый кадр, чтобы проигрывать перемещение в нужный момент.
        _isMove = gameObject.GetComponentInParent<Player_Controller>().IsMove;

        _cd1 = _bazooka.GetComponent<Gun>()._cd;

        _cd2 = GetComponentInParent<Player_Controller>().Couldown2;

        // Передаем данные в аниматор.
        MyAnimCtrl.SetBool("Move", _isMove);        

        // Передаем в аниматор данные о прыжке.
        MyAnimCtrl.SetFloat("Jump", Mathf.Abs(_myRg.velocity.y));

        // Если нажимается атака - активируем анимацию.
        if (Input.GetButtonDown("Fire1") && !_cd1) MyAnimCtrl.SetTrigger("Attack1");

        if (Input.GetButtonDown("Fire2") && !_cd2) MyAnimCtrl.SetTrigger("Attack2");
    }
}
