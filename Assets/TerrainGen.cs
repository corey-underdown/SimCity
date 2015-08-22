using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class TerrainGen : MonoBehaviour {
	const int mapSize	= 1000;
	const int tileSize	= 16;
	public Texture2D grass;
	private int[] map = new int[mapSize*mapSize];

	public Texture2D mapTexture;

	[DllImport("SimCityDLL")]
	private static extern double Add(double a, double b);

	void Start()
	{
		print(Add(1, 2));
		//mapTexture = new Texture2D(100 * 16, 100 * 16, TextureFormat.RGBA32, false, true);
		//mapTexture.filterMode = FilterMode.Point;
		//for (int j = 0; j < tileSize * mapSize; j += 1)
		//{
		//	for (int i = 0; i < tileSize * mapSize; i += 1)
		//	{
		//		PlaceTexture(mapTexture, i, j); // Took 43 seconds
		//		//mapTexture.SetPixel(i, j, GetColor(mapTexture, i, j)); // Took 44 seconds
		//	}
		//}
		//mapTexture.Apply();

		//SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
		//sr.sprite = Sprite.Create(mapTexture, new Rect(0, 0, mapTexture.width, mapTexture.height), new Vector2(0, 0), 10);
	}

	Color GetColor(Texture2D tex, int x, int y)
	{
		return grass.GetPixel(x % 16, y % 16);
	}

	void PlaceTexture(Texture2D tex, int x, int y)
	{
		for(int j = 0; j < tileSize; j++)
		{
			for (int i = 0; i < tileSize; i++)
			{
				tex.SetPixel(x + i, y + j, grass.GetPixel(i, j));
			}
		}
	}


}
