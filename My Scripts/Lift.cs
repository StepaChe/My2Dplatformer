using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Rigidbody2D  _myRg;
    private Vector3 _dir = new Vector3(0, -1, 0);
    public Vector2 maxXAndY;        // The maximum x and y coordinates the camera can have.
    public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
    private bool _up = true;

	// Use this for initialization
	void Start ()
    {
        _myRg = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float targetY = transform.position.y;

        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position += _dir * 50 * Time.deltaTime;
        //transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _up == true)
        {
            _dir.y *= -1;
            //_myRg.AddForce(Vector3.down  * _myRg.mass, ForceMode2D.Impulse);
            transform.position += _dir * 50 * Time.deltaTime;
            _up = false;
        }
        else transform.position += _dir * 50 * Time.deltaTime;
        
    }
}
