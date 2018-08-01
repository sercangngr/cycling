using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kabuk;


public class Settings : MonoBehaviour 
{
	const string InitTimeKey = "init_time";
	const string InitEnergyKey = "init_energy";

	public static float InitTime
	{
		get
		{
			return PlayerPrefs.GetFloat(InitTimeKey, 60 * 4);
		}
	}

	public static float InitEnergy
	{
		get
		{
			return PlayerPrefs.GetFloat(InitEnergyKey, 300);
		}
	}

	public InputField energyInput;
	public InputField timeInput;
	public Text outputText;
	public Button saveButton;
	public Button closeButton;

	private void Awake()
	{
		energyInput.contentType = InputField.ContentType.IntegerNumber;
		timeInput.contentType = InputField.ContentType.IntegerNumber;

		energyInput.text = ((int)InitEnergy) + "";
		timeInput.text = ((int)InitTime) + "";

		saveButton.onClick.AddListener(OnSaveButtonClicked);
		closeButton.onClick.AddListener(() => { Destroy(gameObject); });

		energyInput.onValueChanged.AddListener(OnEnergyChanged);
		timeInput.onValueChanged.AddListener(OnTimeChanged);

		energy = InitEnergy;
		time = InitTime;
	}
    
	float time, energy;

	void OnEnergyChanged(string val)
	{
		energy = int.Parse(val);
		saveButton.image.color = Color.green;
	}

	void OnTimeChanged(string val)
	{
		time = int.Parse(val);
		saveButton.image.color = Color.green;
	}

	void UpdateOutput(float e, float t)
	{
		outputText.text = "Başlangıç Enerjisi: " + e +"\n" + "Başlangıç Zamanı: " + t;
	}


	void OnSaveButtonClicked()
	{
		saveButton.image.color = Color.white;
		PlayerPrefs.SetFloat(InitEnergyKey, energy);
		PlayerPrefs.SetFloat(InitTimeKey, time);
		UpdateOutput(energy, time);
	}






}
