using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_Dropdown qualityDropdown;
    public Toggle vsyncToggle;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        filteredResolutions.Sort((a, b) => {
            if (a.width != b.width)
                return b.width.CompareTo(a.width);
            else
                return b.height.CompareTo(a.height);
        });

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.value.ToString("0.##") + " Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height && (float)filteredResolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);


        //initialize fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;

        //initialize quality settings
        qualityDropdown.ClearOptions();
        List<string> qualityOptions = new List<string>(QualitySettings.names);

        qualityDropdown.AddOptions(qualityOptions);

        int qualityLevel = QualitySettings.GetQualityLevel();
        qualityDropdown.value = qualityLevel;

        qualityDropdown.RefreshShownValue();
        qualityDropdown.onValueChanged.AddListener(SetQuality);


        //initialize V-Sync Toggle
        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;
        vsyncToggle.onValueChanged.AddListener(SetVSync);
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
        UpdateVSyncToggle();
    }

    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
    }

    private void UpdateVSyncToggle()
    {
        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;
    }
}
