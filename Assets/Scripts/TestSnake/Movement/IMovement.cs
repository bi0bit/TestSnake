using UnityEngine;

namespace TestSnake.Snake
{
	public interface IMovement
	{
		void Move(Vector2 moveDirection);
	}
}