namespace LessmoreCase.Game
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class SFXManager : AudioManager<SFXManager>
    {
        private AudioSource AudioSource { get; set; }

        public override void Awake()
        {
            base.Awake();

            this.AudioSource = GetComponent<AudioSource>();
        }

        public void Init()
        {
            SetStatus(Status.ready);
        }

        private void Start()
        {
            LoadSettings();
        }
        public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
        {
            this.AudioSource.PlayOneShot(clip, volumeScale);
        }

        protected override void SetVolume(float volumeScale)
        {
            SetVolume("SFX Volume", volumeScale);

            SaveSettings();
        }
    }
}