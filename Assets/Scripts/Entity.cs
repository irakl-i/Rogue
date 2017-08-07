using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField]
	protected int health;

	// Movement speed.
	[SerializeField]
	[Range(0, 10)]
	protected int speed;

	// How far can entity's hit reach.
	[SerializeField]
	protected int reach;

	// Rigidbody component of the entity.
	protected Rigidbody2D body;

	// Direction the entity is facing.
	protected Vector2 direction;

	public int Health
	{
		get { return health; }
	}

	protected void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}
}