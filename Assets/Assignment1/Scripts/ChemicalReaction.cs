using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalReaction : MonoBehaviour
{
    Draggable draggable;
    public bool conicalFlaskA;
    public Animator animator;
    public AudioSource audioSourcePinkResult;
    public AudioSource audioSourceGreenResult;

    private bool pinkAudioPlayed = false; // Flag for pink result audio
    private bool greenAudioPlayed = false; // Flag for green result audio

    private void Start()
    {
        draggable = GetComponent<Draggable>();
        draggable.chemicalAdded = false;
    }

    private void Update()
    {
        if (conicalFlaskA && draggable.resultAchieved && !pinkAudioPlayed)
        {
            animator.SetBool("ResultPink", true);
            audioSourcePinkResult.PlayOneShot(audioSourcePinkResult.clip);
            pinkAudioPlayed = true; // Set the flag to true to prevent replay
        }
        else if (!conicalFlaskA && draggable.resultAchieved && !greenAudioPlayed)
        {
            animator.SetBool("ResuldGreen", true);
            audioSourceGreenResult.PlayOneShot(audioSourceGreenResult.clip);
            greenAudioPlayed = true; // Set the flag to true to prevent replay
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TestTube")
        {
            Destroy(collision.gameObject);
            draggable.chemicalAdded = true;
        }
    }
}
