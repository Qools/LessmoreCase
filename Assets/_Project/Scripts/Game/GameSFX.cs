namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities;
    using UnityEngine;

    public class GameSFX : Singleton<GameSFX>
    {
        public AudioClip SelectionClip;
        public AudioClip DespawnClip;
        public AudioClip SpawnClip;

        private AudioSource audioSource;
        public override void Awake()
        {
            base.Awake();

            this.audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            SFXManager.Instance.PlayOneShot(clip);
        }

        public void Play(AudioClip clip, float pitch)
        {
            this.audioSource.pitch = pitch;
            this.audioSource.PlayOneShot(clip);
        }
    }
}