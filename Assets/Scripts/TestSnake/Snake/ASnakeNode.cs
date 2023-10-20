using UnityEngine;
using UnityEngine.Events;

namespace TestSnake.Snake
{
	public abstract class ASnakeNode : MonoBehaviour
	{
		public UnityEvent<Collider> OnTriggerEnterNode;
		
		private void OnTriggerEnter(Collider other)
		{
			OnTriggerEnterNode.Invoke(other);
		}
	}
}