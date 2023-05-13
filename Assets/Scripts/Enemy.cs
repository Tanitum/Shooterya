using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;//скорость движения
    [SerializeField] private float _jumpForce = 15f;//сила прыжка

	[SerializeField] private AudioSource _audioSource;//звук получения урона

	private delegate void DeathEventHandler();
	private event DeathEventHandler OnDeath;

    [SerializeField]  private int _health;
	private int health
	{
		get { return _health; }

		set
		{
			_health = value;

			if (health <= 0)
				OnDeath?.Invoke();
		}
	}

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
	
    private void Awake()
    {
		OnDeath += Death;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

	public void TakeDamage(int damage)
	{
		health -= damage;

        _audioSource.Play();
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Player.Singleton.GetDamage();
		}
	}

	private void Death()
	{
		Destroy(gameObject);
	}

    private void OnDestroy()
	{
		OnDeath -= Death;
	}
}