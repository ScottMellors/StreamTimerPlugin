namespace Loupedeck.StreamTimerPlugin
{
    using System;
    using System.Threading.Tasks;

    internal class BumpAdjustment : PluginDynamicAdjustment
    {
        private Int32 _currentBumpValue = 1;
        private Int32 apiState = 0;

        async void SendBumpValue(String uuid, Int32 bumpValue)
        {
            var success = await Tools.PostAsync("action/" + uuid + "/bump/" + bumpValue.ToString());
            
            this.apiState = success == true ? 1 : 2;
            Task.Delay(5000).ContinueWith(t => this.resetApiState());
            this.AdjustmentValueChanged(); 
        }

        public BumpAdjustment() : base("Bump Dial", "Precisely add a bump value to the timer", "Controls", true)
        {            
            
        }

        protected override void RunCommand(String actionParameter)
        {
            this.SendBumpValue(Globals.UserUUID, this._currentBumpValue);
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {
            var label = this._currentBumpValue.ToString();
            if (this.apiState == 0)
            {
                return label;
            } else if (this.apiState == 1)
            {
                label = "✓";
            }
            else if (this.apiState == 2)
            {
                label = "X";
            } else if (this.apiState == 3)
            {
                label = "!";
            }
            return label;
        }

        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var newVal = this._currentBumpValue += ticks;

            if (newVal >= 0)
            {
                this._currentBumpValue = newVal; // increase or decrease counter on the number of ticks
                this.AdjustmentValueChanged();
            }
        }

        public void resetApiState()
        {
            this.apiState = 0;
            this.AdjustmentValueChanged();
        }
    }
}
