using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;//скорость движения
    [SerializeField] private int health = 5;//кол-во здоровья
    [SerializeField] private float jumpForce = 15f;//сила прыжка
	
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
	
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
	
	private void Update()
	{
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	public void TakeDamage(int damage)
	{
		health -= damage;
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == Hero.Instance.gameObject)
		{
			Hero.Instance.GetDamage();
		}
	}
}