using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	public Terrain terrain;
	public RenderTexture heightmap;
	public Camera camera;
	public GameObject brush;
	public ComputeShader shader;

    void Start()
    {
		heightmap = terrain.terrainData.heightmapTexture;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButton(0))
		{
			var Ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(Ray, out hit))
			{
				Vector3 hitpoint = hit.point;
				Debug.Log(hitpoint.ToString());
				brush.transform.position = hit.point;

			}
		}    
    }

	public RenderTexture ApplyMaskToImage(RenderTexture render, Texture2D image, Texture2D mask)
	{
		int kernelIndex = shader.FindKernel("CalculateImage");
		shader.SetTexture(kernelIndex, "Image", render);
		shader.SetTexture(kernelIndex, "Image2D", image);
		shader.SetTexture(kernelIndex, "ImageMask", mask);
		shader.Dispatch(kernelIndex, 32, 32, 1);
		return render;
	}


}
