namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities;
    using UnityEngine;
    using UnityEngine.Audio;

    public abstract class AudioManager<T> : Singleton<T> where T : AudioManager<T>
    {
        [SerializeField]
        private AudioMixer _audioMixer = null;
        private float _volumeScale = 0.0f;

        public float VolumeScale
        {
            get => this._volumeScale;

            set
            {
                this._volumeScale = Mathf.Clamp01(value);

                SetVolume(this._volumeScale);
            }
        }

        protected string PlayerPrefsVolumeKey
        {
            get => string.Format("{0}.{1}", GetType().Name, "Volume");
        }

        protected abstract void SetVolume(float volumeScale);

        protected void SetVolume(string volumeParameterName, float volumeScale)
        {
            volumeScale = Mathf.Clamp01(volumeScale);

            float min = 0.0001f;        
            float max = 1;              

            float linearValue = Mathf.Lerp(min, max, volumeScale);

            float dBValue = 20 * Mathf.Log10(linearValue);

            this._audioMixer.SetFloat(volumeParameterName, dBValue);
        }

        public virtual void SaveSettings()
        {
            PlayerPrefs.SetFloat(this.PlayerPrefsVolumeKey, this.VolumeScale);
            PlayerPrefs.Save();
        }

        protected virtual void LoadSettings()
        {
            if (PlayerPrefs.HasKey(this.PlayerPrefsVolumeKey))
            {
                this.VolumeScale = PlayerPrefs.GetFloat(this.PlayerPrefsVolumeKey);
            }
            else
            {
                this.VolumeScale = 1.0f;
            }
        }
    }

}