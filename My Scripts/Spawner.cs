using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _spawnPoint1;
    [SerializeField] private Transform _spawnPoint2;
    [SerializeField] private Transform _spawnPoint3;
    [SerializeField] private Transform _spawnPoint4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
            Instantiate(_enemy, _spawnPoint1.position, _spawnPoint1.rotation);
            Instantiate(_enemy, _spawnPoint2.position, _spawnPoint2.rotation);
            Instantiate(_enemy, _spawnPoint3.position, _spawnPoint3.rotation);
            Instantiate(_enemy, _spawnPoint4.position, _spawnPoint4.rotation);
            Destroy(gameObject);
        }
    }
}
