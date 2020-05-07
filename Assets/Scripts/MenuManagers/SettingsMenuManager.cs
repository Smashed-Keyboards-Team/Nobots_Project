using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject settingsPanel;

	public Dropdown resolutionDropdown;

	// Mezclador de audio
	public AudioMixer audioMixer;

	public Slider masterSlider;
	public Slider musicSlider;
	public Slider soundSlider;

	// Array de resoluciones
	Resolution[] resolutions;

    public GameObject videoSettings;
    public GameObject audioSettings;
    public GameObject controlSettings;

    private void Start()
    {
		// Detectar las resoluciones del monitor
		resolutions = Screen.resolutions;
		// Limpiar el dropdown
		resolutionDropdown.ClearOptions();
		int currentResolutionIndex = 0;
		// Lista de strings que seran nuestras opciones
		List<string> options = new List<string>();
		// Pasar por todas las resoluciones y añadirlas a options
		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);

			// Detectar resolucion default del monitor y guardarla
			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}
		// Añadir las resoluciones al dropdown
		resolutionDropdown.AddOptions(options);
		// Poner la resolucion default del monitor
		resolutionDropdown.value = currentResolutionIndex;
		// Enseñar lo de arriba
		resolutionDropdown.RefreshShownValue();

		// Activar opciones de video
		videoSettings.SetActive(true);
    }

    // Abrir opciones de video
    public void OpenVideoSettings()
    {
        videoSettings.SetActive(true);
        audioSettings.SetActive(false);
        controlSettings.SetActive(false);
    }
    // Abrir opciones de audio
    public void OpenAudioSettings()
    {
        videoSettings.SetActive(false);
        audioSettings.SetActive(true);
        controlSettings.SetActive(false);
    }
    // Abrir opciones de control
    public void OpenControlSettings()
    {
        videoSettings.SetActive(false);
        audioSettings.SetActive(false);
        controlSettings.SetActive(true);
    }
    // Salir del panel de opciones
    public void ExitSettings()
    {
        settingsPanel.SetActive(false); ;
    }

	// Modificar valores de audio
	public void SetMasterVolume()  //(float sliderValue)
	{
		float sliderValue = masterSlider.value;
		audioMixer.SetFloat("MasterVol", Mathf.Log10 (sliderValue) * 20);
	}
	public void SetMusicVolume()  //(float sliderValue)
	{
		float sliderValue = musicSlider.value;
		audioMixer.SetFloat("MusicVol", Mathf.Log10 (sliderValue) * 20);
	}
	public void SetSoundVolume() //(float sliderValue)
	{
		float sliderValue = soundSlider.value;
		audioMixer.SetFloat("SoundVol", Mathf.Log10 (sliderValue) * 20);
	}

	// Modificar calidad grafica
	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	// Modificar pantalla completa
	public void SetFullScreen (bool isFullscreen)
	{
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
		//Screen.fullScreen = isFullscreen;
	}

	// Modificar resolucion
	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
}
