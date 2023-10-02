using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float _bullet_speed = 25.0f;
    private Vector2 _velocity;
    //[SerializeField] private Rigidbody2D _rb2d;
    private Vector2 _delta = new Vector2(0.0f, 0.0f);
    void Start()
    {
        _velocity = new Vector2(_bullet_speed, _bullet_speed);
        // _rb2d = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        transform.Translate(_delta * Time.deltaTime);
    }

    public void Shoot(Vector2 vec)
    {
        _delta = vec * _bullet_speed;
        
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
        }
        
    }   
}
