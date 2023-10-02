using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_script : MonoBehaviour
{
    int _health = 100;
    float _radius = 5f;
    float _speed = 2f;
    GameObject _playerObject;
    Vector2 _direction;
    Vector2 _Random_angle = new Vector2 (-25f, 60f);
    float _timer = 1.5f; 
    Vector2 _avoidanceVector;
    private LayerMask _obstacleLayer;
    [SerializeField] private Image _Health_bar;

    [SerializeField] GameObject _Ak;
    [SerializeField] private GameObject _Makar;
    [SerializeField] private GameObject _Ammo;

    private bool _died = false;

    void Awake()
    {
        _playerObject = GameObject.FindWithTag("player");
        _obstacleLayer = LayerMask.GetMask("Default");
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, 2f, _obstacleLayer);
        if (hit.collider != null)
        {
            _avoidanceVector = Vector2.Reflect(_direction, hit.normal).normalized;
        }
        else
        {
            _avoidanceVector = Vector2.zero;
        }

        if (Vector2.Distance(transform.position, _playerObject.transform.position) < _radius)
        {

            _direction = _playerObject.transform.position - transform.position;
            _direction = (_direction + _avoidanceVector).normalized;
            //transform.position = Vector2.MoveTowards(this.transform.position, _playerObject.transform.position, _speed * Time.deltaTime);
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
        else
        {
            _timer -= Time.deltaTime;
            if( _timer <= 0 )
            {
                _timer = 1.5f;
                generateAnge();
            }
            _direction = (_Random_angle.normalized + _avoidanceVector).normalized;
            transform.Translate(_direction * _speed/2 * Time.deltaTime);
            
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            _health -= 20;
            _Health_bar.fillAmount = _health * 0.01f;
        }
        if (_health <= 0)
        {
            if (!_died)
            {
                _died = true;
                DropItem();
                GManager.Instance.EnemyKilled();
            }
            Destroy(gameObject);
        }
    }

    private void generateAnge()
    {
        float randomValueX = (Random.Range(0, 2) == 0) ? Random.Range(-100f, -20f) : Random.Range(60f, 100f);
        float randomValueY = (Random.Range(0, 2) == 0) ? Random.Range(-100f, -10f) : Random.Range(20f, 100f);
        _Random_angle = new Vector2(randomValueX, randomValueY);
    }

    private void DropItem()
    {
        int num = Random.Range(0, 3);
        switch (num)
        {
            case 0:
                Instantiate(_Ak, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(_Makar, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(_Ammo, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

    }
}
