using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static itemScript;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class playerMove : MonoBehaviour
{

    public MovementJoystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    private float _health;
    [SerializeField] private Image _Health_bar;
    private bool _beeingHit = false;
    private float _timer = 1.5f;

    [SerializeField] GameObject _bulletTemplate;
    [SerializeField] float _Shooting_Offset = 0.8f;
    Vector2 Faicinng = new Vector2 (1f,1f);
    [SerializeField] GameObject _GunPointion;

    private bool _IAMDEAD = false;

    [SerializeField] float shootRadius = 5f;

    void Start()
    {
        _health = 100f;
        rb = GetComponent<Rigidbody2D>();
        GManager.Instance.LoadData();
        _health = GManager.Instance.ReturnHealth();
        _Health_bar.fillAmount = _health * 0.01f;
    }

 
    void FixedUpdate()
    {
        if (_IAMDEAD)
        {
            return;
        }
        if (movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
            Faicinng = new Vector2(movementJoystick.joystickVec.x, movementJoystick.joystickVec.y).normalized;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        if(_beeingHit)
        {
            _timer-= Time.deltaTime;
        }
        if (_timer <= 0)
        {
            _timer = 1.5f;
            LoseHealth();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_IAMDEAD)
        {
            return;
        }
        if (collision.gameObject.CompareTag("monster"))
        {
            LoseHealth();
            _beeingHit = true;
        }
    }
    public float ReturnHealth()
    {
        return _health;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("monster"))
        {
            _beeingHit = false;
        }
    }

    private void LoseHealth()
    {
        _health -= 20;
        _Health_bar.fillAmount = _health * 0.01f;
        if (_health <= 0)
        {
            GManager.Instance.PlayerDied();
            _IAMDEAD = true;
            rb.velocity = Vector2.zero;
            //gameover
        }
        Debug.Log(_health);
    }

    public void Shoot()
    {
        if (_IAMDEAD || backpack.Instance.CheakAmmunition() == 0)
        {
            return;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, shootRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("monster"))
            {
                Vector2 point = Faicinng * _Shooting_Offset;
                Vector2 _Spawn_Point = new Vector2(_GunPointion.transform.position.x + 0.924f, _GunPointion.transform.position.y + .102f) + Faicinng;
                GameObject my_bullet = Instantiate(_bulletTemplate, _Spawn_Point, transform.rotation);
                my_bullet.GetComponent<bullet>().Shoot(point);
                backpack.Instance.LoseDBullet();
                break; // Exit the loop since we found a monster
            }
        }
    }
}