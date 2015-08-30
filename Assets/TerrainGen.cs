using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class TerrainGen : MonoBehaviour {
	public int mapSize;
	const int tileSize	= 16;
	public Texture2D tilesheet;
	public int[] map;
	private int spriteSheetTilesX;
	private int spriteSheetTilesY;

	private Texture2D mapTexture;

	//Tiles
	const int grassTile = 2;
	const int waterTile = 3;

	//[DllImport("SimCityDLL")]
	//private static extern double Add(double a, double b);

	void Awake()
	{
		map = new int[mapSize * mapSize];
		spriteSheetTilesX = tilesheet.width / tileSize;
		spriteSheetTilesY = tilesheet.height / tileSize;

	}

	void Start()
	{
		GenIsland();
		CreateSprite();
	}
	
	void GenIsland()
	{
		for(int i = 0; i < map.Length; i++)
		{
			if (i % mapSize == 0 || i % mapSize == mapSize - 1 || i / mapSize == 0 || i / mapSize == mapSize - 1)
				map[i] = waterTile;
			else
				map[i] = grassTile;
		}
	}

	void CreateSprite()
	{
		mapTexture = new Texture2D(mapSize * 16, mapSize * 16, TextureFormat.RGBA32, false, true);
		mapTexture.filterMode = FilterMode.Point;
		for (int j = 0; j < mapSize; j += 1)
		{
			for (int i = 0; i < mapSize; i += 1)
			{
				PlaceTexture(mapTexture, i, j); // Took 43 seconds
				//mapTexture.SetPixel(i, j, GetColor(i, j)); // Took 44 seconds
			}
		}
		mapTexture.Apply();

		SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = Sprite.Create(mapTexture, new Rect(0, 0, mapTexture.width, mapTexture.height), new Vector2(0, 0), 10);
	}

	Color GetColor(int x, int y)
	{
		return tilesheet.GetPixel(x % 16 + 16, y % 16);
	}

	void PlaceTexture(Texture2D tex, int x, int y)
	{
		Vector2 temp = ConvertIDToPos(map[x + y * mapSize]);
		for(int j = 0; j < tileSize; j++)
		{
			for (int i = 0; i < tileSize; i++)
			{
				tex.SetPixel((x << 4) + i, (y << 4) + j, tilesheet.GetPixel(i + 16 * (int)temp.x, j + 16 * (int)temp.y));
			}
		}
	}

	Vector2 ConvertIDToPos(int id)
	{
		int x = id % spriteSheetTilesX;
		int y = id / spriteSheetTilesY;
		return new Vector2(x, spriteSheetTilesY - 1 - y);
	}


}
