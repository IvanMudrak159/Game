using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
	public Text _missionDescriptionText;
	public GameObject _missionPanel;
	public static Text missionDescriptionText;
	public static GameObject missionPanel;
	public Button disableMissionPanel;
	private void Awake()
	{
		missionPanel = _missionPanel;
		missionDescriptionText = _missionDescriptionText;
	}
	public void DisablePanel()
	{
		Debug.Log("Sosat");
		missionPanel.SetActive(false);
	}
}