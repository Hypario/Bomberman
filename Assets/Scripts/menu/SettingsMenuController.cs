using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


// script for the Settings Menu
public class SettingsMenuController : MonoBehaviour
{

    // needed to turn up or turn down the volume
    public AudioMixer gameVolumeMixer;

    // all the resolutions of the screen that Unity provide
    Resolution[] resolutions;

    // the dropdown for the resolutions
    public Dropdown resolutionDropdown;

    // the full screen button
    public Toggle FullScreenButton;

    private void Start()
    {
        resolutions = Screen.resolutions; // fill up the resolution array

        resolutionDropdown.ClearOptions(); // clear the options by default in the dropdown

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        // fill up the list of options
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // fill up the resolution dropdown using the options list with a certain format
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex; // set the resolution of the screen
        resolutionDropdown.RefreshShownValue();

        FullScreenButton.SetIsOnWithoutNotify(Screen.fullScreen); // set true or false if in fullscreen
    }

    // set the resolution selected
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // set the volume selected
    public void SetVolume(float volume)
    {
        gameVolumeMixer.SetFloat("gameVolume", volume);
    }

    // set full screen if selected
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
