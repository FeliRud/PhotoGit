using System;

namespace Photo
{
    [Serializable]
    public class RulesData
    {
        public bool StartRules;
        public bool PhotoRules;
        public bool InteractableRules;
        public bool FireflyRules;

        public RulesData()
        {
            StartRules = false;
            PhotoRules = false;
            InteractableRules = false;
            FireflyRules = false;
        }

        public void StartRulesCompleted() => 
            StartRules = true;
        public void PhotoRulesCompleted() => 
            PhotoRules = true;
        public void InteractableRulesCompleted() => 
            InteractableRules = true;
        public void FireflyRulesCompleted() => 
            FireflyRules = true;
    }
}