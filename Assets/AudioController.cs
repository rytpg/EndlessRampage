using UnityEngine;
using UnityEngine.UI;

/*
This script handles the switching of the image of the volume button
based on its state (muted / unmuted)
*/
public class AudioController : MonoBehaviour
{

    public AudioSource musicSource;
    public Image buttonImage;

    public Sprite unmutedSprite;
    public Sprite mutedSprite;

    private bool isMuted = false;

    public void ToggleMusic()
    {
        isMuted = !isMuted;

        musicSource.mute = isMuted;

        if (isMuted)
        {
            buttonImage.sprite = mutedSprite;
        }
        else
        {
            buttonImage.sprite = unmutedSprite;
        }
    }
}
