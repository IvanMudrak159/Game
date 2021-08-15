using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
	public GameObject _missionPanel;
	public Text _missionDescriptionText;
	public Button disableMissionPanel;

	public static Text missionDescriptionText;
	public static GameObject missionPanel;
	private void Awake()
	{
		missionPanel = _missionPanel;
		missionDescriptionText = _missionDescriptionText;
	}
	public void DisablePanel()
	{
		missionPanel.SetActive(false);
	}
}