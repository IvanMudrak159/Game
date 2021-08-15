using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSkin : MonoBehaviour
{
    public string key;
    public Material material;
	public Texture[] textures;
	public Material[] materials;
	public bool isMaterial;
	public void Awake()
	{
		if (isMaterial)
		{
			GetComponent<MeshRenderer>().material = materials[PlayerPrefs.GetInt(key, 0)];
		}
		else
		{
		material.mainTexture = textures[PlayerPrefs.GetInt(key, 0)];
		}
	}
}