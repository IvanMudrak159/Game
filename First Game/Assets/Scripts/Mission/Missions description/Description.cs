using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Description : ScriptableObject
{
	public string playerPrefsKey;
	public string name;
	public string description;

}
