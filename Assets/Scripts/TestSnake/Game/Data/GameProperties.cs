using TestSnake.Food;
using UnityEngine;

namespace TestSnake.Game.Data
{
	[CreateAssetMenu(fileName = nameof(GameProperties), menuName = "Parameters/"+nameof(GameProperties), order = 0)]
	public class GameProperties : ScriptableObject
	{
		[field: SerializeField] public int StartFoodCount { get; private set; }
		[field: SerializeField] public AFood FoodPrefab { get; private set; }
	}
}