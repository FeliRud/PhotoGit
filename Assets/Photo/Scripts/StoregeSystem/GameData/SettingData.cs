namespace Photo
{
    [System.Serializable]
    public class SettingData
    {
        public float SoundValue;

        public SettingData()
        {
            SoundValue = 1f;
        }

        public void SoundValueChanged(float value)
        {
            SoundValue = value;
        }
    }
}