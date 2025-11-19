using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip soundClip;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
        }
    }
}
