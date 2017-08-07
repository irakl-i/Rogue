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

	// Direction the entity is facing.
	protected Vector2 direction;

	protected Rigidbody2D body;
	protected new SpriteRenderer renderer;

	public int Health
	{
		get { return health; }
	}

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer>();
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
		Color original = renderer.color;

		renderer.color = Color.white;
		Debug.Log("Flashing");
		yield return new WaitForSeconds(FlashDelay);
		renderer.color = original;
		yield return new WaitForSeconds(FlashDelay);
	}
}