﻿using System;
using System.Collections.Generic;
using TestSnake.Food;
using TestSnake.Map;
using TestSnake.Snake.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TestSnake.Snake.Impl
{
	public class Snake : MonoBehaviour, ISnake
	{
		[SerializeField]
		private ASnakeNode _head;

		[SerializeField]
		private ASnakeNode _bodyPrefab;

		private readonly LinkedList<ASnakeNode> _body = new();
		
		[field: SerializeField] public SnakeParameters Data { get; private set; }

		public ICollection<ASnakeNode> Body => _body.AsReadOnlyCollection();
		
		public IMovement Movement { get; private set; }
		
		public IEater Eater { get; private set; }

		public event UnityAction OnGrow;

		private SphereCollider _collider;

		private void Awake()
		{
			InitMovement();
			
			InitEater();
			
			_head.OnTriggerEnterNode.AddListener(OnTriggerEnterHead);

			_body.AddFirst(_head);
			
			InitBodyLength();
		}

		private void InitMovement()
		{
			Movement = new SnakeMovement(this);
		}

		private void InitEater()
		{
			Eater = new SnakeEater(this);
		}

		private void InitBodyLength()
		{
			for (var i = 0; i < Data.StartBodyLength; ++i)
			{
				Grow();
			}
		}

		public void Grow()
		{
			var lastNodeTransform = _body.Last.Value.transform;
			var newNodePosition = lastNodeTransform.position + (lastNodeTransform.forward * -1) * Data.BodySpace;
			var newNode = Instantiate(_bodyPrefab, newNodePosition, lastNodeTransform.rotation, transform);
			_body.AddLast(newNode);
			
			OnGrow?.Invoke();
		}


		private void OnTriggerEnterHead(Collider other)
		{
			if (other.TryGetComponent(out AFood food))
			{
				Eater.TryEat(food);
			}
		}
		
	}
}