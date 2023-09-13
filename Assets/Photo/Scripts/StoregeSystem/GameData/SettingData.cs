namespace Photo
{
    [System.Serializable]
    public class SettingData
    {
        public RulesData RulesIsCompleted;
        public float SoundValue;

        public SettingData()
        {
            RulesIsCompleted = new RulesData();
            SoundValue = 1f;
        }
        
        public void SoundValueChanged(float value) => 
            SoundValue = value;
    }
}