using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSound : MonoBehaviour
{
    public List<AudioClip> audioClips; // Die Liste der AudioClips
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Package"))
        {
            if (audioClips.Count > 0)
            {
                int randomIndex = Random.Range(0, audioClips.Count);
                AudioClip randomClip = audioClips[randomIndex];

                // Spiele den zufälligen AudioClip ab
                audioSource.PlayOneShot(randomClip);
            }
        }
    }
}
