using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VFX;
public class texture2D23D : MonoBehaviour
{
	public string host = "http://127.0.0.1:5000/";
	public string fileName = "tornado3d.uvw";

	public VisualEffect vfx;
	public GameObject boxVolume;
	private Texture3D texture3D;
	private byte[] results;
	private int xdim=0;
	private int ydim=0;
	private int zdim=0;
	private string tensorpath = "tensor?name=";
	private string shapepath = "shape?name=";

	void Start()
	{
		StartCoroutine(GetTextureData());
	}

	IEnumerator GetTextureData()
	{
		Debug.Log("shape= " + host + shapepath + fileName);
        UnityWebRequest www = UnityWebRequest.Get(host + shapepath + fileName);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError) {
            Debug.Log(www.error);
        }
        else {
			results = www.downloadHandler.data;
			xdim = results[0];  
			ydim = results[1];
			zdim = results[2];
		}

		Debug.Log("shape = " + xdim + "," + ydim + "," + zdim);
		Debug.Log("request= " + host + tensorpath + fileName);

        www = UnityWebRequest.Get(host + tensorpath + fileName);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError) {
            Debug.Log(www.error);
        }
        else {
            // retrieve results as binary data
            results = www.downloadHandler.data;
			texture3D = Create3DTexture(xdim,ydim,zdim,results);
			vfx.SetTexture("New Texture3D", texture3D);
			if(boxVolume != null)
				boxVolume.GetComponent<Renderer>().material.SetTexture("_MainTex", texture3D);
        }
    }

	// this function creates a texture3d from an array of bytes
	public Texture3D Create3DTexture(int width, int height, int depth, byte[] data)
	{
        Texture3D tex3D = new Texture3D(width, height, depth, TextureFormat.RGB24, false);
		tex3D.wrapMode = TextureWrapMode.Repeat;
		tex3D.filterMode = FilterMode.Point;
		tex3D.anisoLevel = 0;

		tex3D.SetPixelData(data, 0);
        tex3D.Apply(updateMipmaps: false);

		return tex3D;
	}
	
	// this function creates a texture3d from an array of texture2Ds
	public Texture3D Create3DTexture(Texture2D[] textures)
	{
		int width = Mathf.ClosestPowerOfTwo(textures[0].width);
		int height = Mathf.ClosestPowerOfTwo(textures[0].height);
		int depth = Mathf.ClosestPowerOfTwo(textures.Length);

		Color c = Color.white;

		Texture2D tex2D = new Texture2D(textures[0].width,textures[0].height);
        Texture3D tex3D = new Texture3D(width, height, depth, TextureFormat.RGB24, false);
		tex3D.wrapMode = TextureWrapMode.Repeat;
		tex3D.filterMode = FilterMode.Point;
		tex3D.anisoLevel = 0;

		Color[] cols = new Color[width * height * depth];

		int ix = 0;
		for ( int z = 0; z < depth; z++ )
		{
			tex2D = textures[z];

			for ( int x = 0; x < width; x++ )
			{
				int xindex = (int)((float)x / (float)width) * tex2D.width;

				for ( int y = 0; y < height; y++ )
				{
					int yindex = (int)((float)y / (float)height) * tex2D.height;
					c = tex2D.GetPixel(xindex,yindex);
					cols[ix++] = c;
				}
			}
		}

        tex3D.SetPixels(cols);
		tex3D.Apply();
        return tex3D;
    }
}
