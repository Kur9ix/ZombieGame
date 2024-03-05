using UnityEngine;
using UnityEngine.PlayerLoop;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    
    [Range(1, 1000)]
    public float scale = 20f;
    [Range(1, 1000)]
    public float RangeX;

    [Range(1, 1000)]
    public float RangeY;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //renderer.material.mainTexture = GenerateTexture();
    }

    public Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = GenerateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color GenerateColor(int x, int y)
    {

        float xCoord = (float)x / width * scale + RangeX;
        float yCoord = (float)y / width * scale + RangeY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }

    public float GeneratePerlinNoiseValue(int x, int y)
    {
        float xCoord = (float)x / width * scale + RangeX;
        float yCoord = (float)y / height * scale + RangeY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

}

