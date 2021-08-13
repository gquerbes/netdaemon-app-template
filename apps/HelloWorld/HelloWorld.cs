using System;
using System.Reactive.Linq;
using daemonapp;
using NetDaemon.Common.Reactive;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace HelloWorld
{
    /// <summary>
    ///     Hello world showcase
    /// </summary>
    public class HelloWorldApp : NetDaemonRxApp
    {

        TradfriRemote remote;
        public override void Initialize()
        {
            RegisterToControlEvents();
            
            //subscribe for ikea button events
            EventChanges
                //listen for all zigbee home automation events (zhe_event)
                .Where(e => e.Event.Contains("zha_event"))
                .Subscribe(s =>
               {

                   remote.ProcessCommand(s);

               });


     

        }

        private void RegisterToControlEvents()
        {
            remote = new TradfriRemote();
            remote.OnLeftButtonPressed += OnLeftButtonPressed;
            remote.OnRightButtonPressed+= OnRightButtonPressed;
            remote.OnUpButtonPressed += OnUpButtonPressed;
            remote.OnDownButtonPressed += OnDownButtonPressed;
            remote.OnCenterButtonPressed += OnCenterButtonPressed;


            remote.OnLeftButtonHeld += OnLeftButtonHold;
            remote.OnRightButtonHeld += OnRightButtonHold;
            remote.OnUpButtonHeld += OnUpButtonHold;
            remote.OnDownButtonHeld += OnDownButtonHold;
            remote.OnCenterHeld += OnCenterButtonHold;

            remote.OnLeftButtonReleased += OnLeftButtonReleased;
            remote.OnRightButtonReleased += OnRightButtonReleased;
            remote.OnUpButtonReleased += OnUpButtonReleased;
            remote.OnDownButtonReleased += OnDownButtonReleased;

        }

      

        private void OnLeftButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("on Left Button click");

        }

        private void OnLeftButtonHold(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Left Button hold {timeHeld}");
        }

        private void OnLeftButtonReleased(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Left Button released {timeHeld}");
        }

        private void OnRightButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Right Button Pressed");
        }

        private void OnRightButtonHold(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Right Button hold {timeHeld}");
        }

        private void OnRightButtonReleased(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Right Button released {timeHeld}");
        }

        private void OnUpButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Up Button Pressed");

        }
        private void OnUpButtonHold(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Up Button hold {timeHeld}");
        }

        private void OnUpButtonReleased(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Up Button released {timeHeld} ");
        }

        private void OnDownButtonHold(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Down Button hold {timeHeld}");
        }

        private void OnCenterButtonHold(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Down Button hold {timeHeld}");
        }

        private void OnDownButtonReleased(double timeHeld)
        {
            System.Diagnostics.Debug.WriteLine($"Down Button released {timeHeld}");
        }


        private void OnDownButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Down Button Pressed");

        }

        private void OnCenterButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Center Button Pressed");
          
        }

     

    }
}
