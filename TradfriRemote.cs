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

        public delegate void LeftButtonHeld();
        public delegate void RightButtonHeld();
        public delegate void UpButtonHeld();
        public delegate void DownButtonHeld();
        public delegate void CenterHeld();

        public delegate void LeftButtonReleased();
        public delegate void RightButtonReleased();
        public delegate void UpButtonReleased();
        public delegate void DownButtonReleased();

        public LeftButtonPressed OnLeftButtonPressed;
        public RightButtonPressed OnRightButtonPressed;
        public UpButtonPressed OnUpButtonPressed;
        public DownButtonPressed OnDownButtonPressed;
        public CenterButtonPressed OnCenterButtonPressed;

        public LeftButtonHeld OnLeftButtonHeld;
        public RightButtonHeld OnRightButtonHeld;
        public UpButtonHeld OnUpButtonHeld;
        public DownButtonHeld OnDownButtonHeld;
        public CenterHeld OnCenterHeld;

        public LeftButtonReleased OnLeftButtonReleased;
        public RightButtonReleased OnRightButtonReleased;
        public UpButtonReleased OnUpButtonReleased;
        public DownButtonReleased OnDownButtonReleased;
        #endregion Events
    }
}
