namespace Photo
{
    [System.Serializable]
    public class GameData
    {
        public ProgressData Progress;
        public SettingData Setting;

        public GameData()
        {
            Progress = new ProgressData();
            Setting = new SettingData();
        }
    }
}