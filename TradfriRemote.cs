using NetDaemon.Common.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daemonapp
{
    public class TradfriRemote
    {

        #region Events
        public delegate void LeftButtonPressed();
        public delegate void RightButtonPressed();
        public delegate void UpButtonPressed();
        public delegate void DownButtonPressed();
        public delegate void CenterButtonPressed();

        public delegate void LeftButtonHeld(double timeHeld);
        public delegate void RightButtonHeld(double timeHeld);
        public delegate void UpButtonHeld(double timeHeld);
        public delegate void DownButtonHeld(double timeHeld);
        public delegate void CenterHeld(double timeHeld);

        public delegate void LeftButtonReleased(double timeHeld);
        public delegate void RightButtonReleased(double timeHeld);
        public delegate void UpButtonReleased(double timeHeld);
        public delegate void DownButtonReleased(double timeHeld);

        public LeftButtonPressed? OnLeftButtonPressed;
        public RightButtonPressed? OnRightButtonPressed;
        public UpButtonPressed? OnUpButtonPressed;
        public DownButtonPressed? OnDownButtonPressed;
        public CenterButtonPressed? OnCenterButtonPressed;

        public LeftButtonHeld? OnLeftButtonHeld;
        public RightButtonHeld? OnRightButtonHeld;
        public UpButtonHeld? OnUpButtonHeld;
        public DownButtonHeld? OnDownButtonHeld;
        public CenterHeld? OnCenterHeld;

        public LeftButtonReleased? OnLeftButtonReleased;
        public RightButtonReleased? OnRightButtonReleased;
        public UpButtonReleased? OnUpButtonReleased;
        public DownButtonReleased? OnDownButtonReleased;
        #endregion Events

        private string HeldDirectionalButton = "";
        private string HeldBrightnessButton = "";


        public void ProcessCommand(RxEvent s)
        {
            if (s?.Data == null)
                return;
            double args = 0;
            string command = "";

            try
            {
                args = s?.Data["args"]?[0];
            }
            catch { /*do nothing and continue*/};


            try
            {
                command = s?.Data?["command"];
            }
            catch
            {
                //there was no command. (likely device went to sleep)
                return;
            }

            //get command from event
            switch (command)
            {
                //press of left or right button
                case "press":
                    HandlePressEvents(args);
                    break;
                //hold of a directional button
                case "hold":
                    HandleHoldEvent(args);
                    break;
                //release of left or right button
                case "release":
                    HandleReleaseEvents(args);
                    break;
                //press brightness Down
                case "step":
                    OnDownButtonPressed?.Invoke();
                    break;
                //press brightness up
                case "step_with_on_off":
                    OnUpButtonPressed?.Invoke();
                    break;
                //hold brightness down
                case "move":
                    HeldBrightnessButton = "DOWN";
                    OnUpButtonHeld?.Invoke(args);
                    break;
                // hold brightness down
                case "move_with_on_off":
                    HeldBrightnessButton = "UP";
                    OnDownButtonHeld?.Invoke(args);
                    break;
                // release of brightness buttons
                case "stop":
                    HandleStopEvents(args);
                    break;
                case "toggle"://power button
                    OnCenterButtonPressed?.Invoke();
                    break;
            }

        }

        /// <summary>
        /// Handle releasing of directional button
        /// </summary>
        private void HandleReleaseEvents(double timeHeld)
        {
            if (HeldDirectionalButton == "LEFT")
            {
                OnLeftButtonReleased?.Invoke(timeHeld);
            }
            else if (HeldDirectionalButton == "RIGHT")
            {
                OnRightButtonReleased?.Invoke(timeHeld);
            }

            System.Diagnostics.Debug.WriteLine($"*Time {timeHeld}");
        }

        /// <summary>
        /// Handle release of brightness button
        /// </summary>
        private void HandleStopEvents(double timeHeld)
        {
            if (HeldBrightnessButton == "UP")
            {
                OnUpButtonReleased?.Invoke(timeHeld);
            }
            else if (HeldBrightnessButton == "DOWN")
            {
                OnDownButtonReleased?.Invoke(timeHeld);
            }
            System.Diagnostics.Debug.WriteLine($"*Time {timeHeld}");

        }

        private void HandlePressEvents(double deviceId)
        {
            switch (deviceId)
            {
                case 256://right button tap
                    OnRightButtonPressed?.Invoke();
                    break;
                case 257://left button tap
                    OnLeftButtonPressed?.Invoke();
                    break;
            };
        }

        private void HandleHoldEvent(double timeHeld)
        {
            switch (timeHeld)
            {
                case 3328://right button tap
                    OnRightButtonHeld?.Invoke(timeHeld);
                    HeldDirectionalButton = "RIGHT";
                    break;
                case 3329://left button tap
                    OnLeftButtonHeld?.Invoke(timeHeld);
                    HeldDirectionalButton = "LEFT";
                    break;
            };
        }

    }
}
