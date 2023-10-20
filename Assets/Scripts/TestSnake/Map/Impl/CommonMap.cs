using UnityEngine;

namespace TestSnake.Map.Impl
{
	public class CommonMap : MonoBehaviour, IMap
	{
		public Mesh Mesh { get; private set; }

		private void Awake()
		{
			var meshFilters = GetComponentsInChildren<MeshFilter>();
			var combine = new CombineInstance[meshFilters.Length];

			int i = 0;
			while (i < meshFilters.Length)
			{
				combine[i].mesh = meshFilters[i].sharedMesh;
				combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

				i++;
			}

			var mesh = new Mesh();
			mesh.CombineMeshes(combine);
			
			Mesh = mesh;
		}

	}
}