using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
	public float power = 3;
	public float scale = 1;
	public float timeScale = 1;

	private float offsetX;
	private float offsetY;
	private MeshFilter mf;

	private void Awake()
	{
		mf = GetComponent<MeshFilter>();
	}

    private void Start()
    {
		MakeNoise();
	}

    private void Update()
	{
		MakeNoise();
		offsetX += Time.deltaTime * timeScale;
		offsetY += Time.deltaTime * timeScale;
	}

	private void MakeNoise()
	{
		Vector3[] verticies = mf.mesh.vertices;
		for (int i = 0; i < verticies.Length; i++)
		{
			verticies[i].y = CalculateHeight(verticies[i].x, verticies[i].z) * power;
		}
		mf.mesh.vertices = verticies;
	}

	private float CalculateHeight(float x, float y)
	{
		float xCord = x * scale + offsetX;
		float yCord = y * scale + offsetY;
		return Mathf.PerlinNoise(xCord, yCord);
	}
}