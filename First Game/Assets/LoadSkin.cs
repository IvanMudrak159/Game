using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSkin : MonoBehaviour
{
    public string key;
    public Material material;
	public Texture[] textures;
	public void Awake()
	{
		Debug.Log(key + PlayerPrefs.GetInt(key, 0));
		material.mainTexture = textures[PlayerPrefs.GetInt(key, 0)];
	}
}