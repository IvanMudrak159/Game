using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDescription : MonoBehaviour
{
	public Description mission;

	public Text missionName;
	private void Awake()
	{
		missionName.text = mission.name;
	}
	public void Show()
	{
		MissionPanel.missionPanel.SetActive(true);
		MissionPanel.missionDescriptionText.text = mission.description;
	}
}
