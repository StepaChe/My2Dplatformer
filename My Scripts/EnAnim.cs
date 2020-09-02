using UnityEngine;

public class EnAnim : MonoBehaviour {

    public Animator MyAnimCtrl;
    private bool _isMove;
    private bool _isAngry;

    private Rigidbody2D _myRg;


    // Use this for initialization
    void Start()
    {
        MyAnimCtrl = GetComponent<Animator>();

        _myRg = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Получаем ссылку на логическую переменную родительского объекта каждый кадр, чтобы проигрывать перемещение в нужный момент.
        //_isMove = gameObject.GetComponentInParent<Player_Controller>().IsMove;
        //_isAngry = gameObject.GetComponentInParent<Enemy>().IsAngry;
        // Передаем данные в аниматор.
        MyAnimCtrl.SetBool("Move", _isMove);

        // Передаем в аниматор данные о прыжке.
        //MyAnimCtrl.SetBool("Attack", _isAngry);

        // Если нажимается атака - активируем анимацию.
        if (_isAngry == true) MyAnimCtrl.SetTrigger("Attack");
    }
}
