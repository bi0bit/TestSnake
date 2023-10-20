using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestSnake.Food;
using TestSnake.Snake.Data;
using UnityEngine.Events;

namespace TestSnake.Snake
{
	public interface ISnake
	{
		SnakeParameters Data { get; }
		
		ICollection<ASnakeNode> Body { get; }
		
		IMovement Movement { get; }
		
		IEater Eater { get; }
		
		event UnityAction OnGrow;

		void Grow();
	}
}