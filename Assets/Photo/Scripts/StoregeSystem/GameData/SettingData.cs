namespace Photo
{
    [System.Serializable]
    public class SettingData
    {
        public bool RulesIsCompleted;
        public float SoundValue;

        public SettingData()
        {
            RulesIsCompleted = false;
            SoundValue = 1f;
        }

        public void RulesCompleted()
        {
            RulesIsCompleted = true;
        }
        
        public void SoundValueChanged(float value)
        {
            SoundValue = value;
        }
    }
}