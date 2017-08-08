using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	private const float FlashDelay = 0.1f;

	[Header("Stats")]
	[SerializeField]
	protected int health;

	// Movement speed.
	[SerializeField]
	[Range(0, 10)]
	protected int speed;

	[SerializeField]
	protected int damage;

	// How far can entity's hit reach.
	[SerializeField]
	protected int reach;

	protected new SpriteRenderer renderer;
	protected Rigidbody2D body;
	protected Vector2 facing;
	protected Color original;

	public int Health
	{
		get { return health; }
	}

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer>();
		original = renderer.color;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		Debug.LogFormat("Took {0} damage, {1} health remaining", damage, health);

		StartCoroutine("Flash");
		if (health <= 0)
			Destroy(gameObject);
	}

	private IEnumerator Flash()
	{
		renderer.color = Color.red;
		yield return new WaitForSeconds(FlashDelay);
		renderer.color = original;
		yield return new WaitForSeconds(FlashDelay);
	}
}