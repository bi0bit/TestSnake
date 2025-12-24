using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestSnake.Snake.Impl
{
	public class SnakeMovement : IMovement
	{
		private const float OFFSET_FROM_LAND = .65f;

		private readonly ISnake _snake;
		
		private readonly ICollection<ASnakeNode> _body;
		
		public SnakeMovement(ISnake snake)
		{
			_snake = snake; 
			_body = snake.Body;
		}

		public void Move(Vector2 moveDirection)
		{
			var head = _body.First();
			
			MoveHead(moveDirection, head);
			
			MoveTail(head);
		}

		private void MoveHead(Vector2 moveDirection, ASnakeNode head)
		{
			head.transform.Rotate(Vector3.up, moveDirection.x * _snake.Data.TurnSpeed * Time.deltaTime);
			head.transform.position += head.transform.forward * _snake.Data.MoveSpeed * Time.deltaTime;
		}

		private void MoveTail(ASnakeNode head)
		{
			var lastNode = head;
			foreach (var snakeBodyNode in _body)
			{
				if (snakeBodyNode == head)
				{
					continue;
				}

				var lastNodePosition = lastNode.transform.position;
				var positionToMove = lastNodePosition - lastNode.transform.forward * (_snake.Data.BodySpace / 2);
				var moveDirectionBodyNode = (positionToMove - snakeBodyNode.transform.position).normalized;

				snakeBodyNode.transform.position += moveDirectionBodyNode * _snake.Data.BodySpeed * Time.deltaTime;
				snakeBodyNode.transform.LookAt(lastNodePosition);

				lastNode = snakeBodyNode;
			}
		}

		public void SnapOnLand()
		{
				var head = _body.First().transform;

				var ray = new Ray(head.position + head.up * 1, head.up * -1);

				if (!Physics.Raycast(ray, out var hit, int.MaxValue, LayerMaskGame.Map)) return;
				
				head.position = hit.point + head.up * OFFSET_FROM_LAND;

				var alignedRotation = Quaternion.FromToRotation(head.up, hit.normal);
				head.rotation = alignedRotation * head.rotation;
				
		}

	}
}