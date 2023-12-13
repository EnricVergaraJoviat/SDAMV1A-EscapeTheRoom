using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        int randomNumber;
        randomNumber = Random.Range(0, 5);
        _audioSource.PlayOneShot(clips[randomNumber]);
        Debug.Log(randomNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
