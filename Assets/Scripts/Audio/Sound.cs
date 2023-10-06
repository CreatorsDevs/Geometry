using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // The name identifier for the sound

    public AudioClip clip; // The audio clip to be played

    [Range(0f, 1f)]
    public float volume; // The volume level of the sound (0 to 1)

    [Range(.1f, 3f)]
    public float pitch; // The pitch of the sound (0.1 to 3)

    public bool loop; // Whether the sound should loop

    [HideInInspector]
    public AudioSource source; // The AudioSource component used to play the sound
}