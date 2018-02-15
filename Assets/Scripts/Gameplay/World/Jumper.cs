using UnityEngine;

public class Jumper : MonoBehaviour
{
	[SerializeField] 
	private float jumpForce;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		other.attachedRigidbody.AddForce(new Vector2(0, jumpForce));
	}
}
