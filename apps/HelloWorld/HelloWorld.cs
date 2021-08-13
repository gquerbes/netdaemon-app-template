using System;
using System.Reactive.Linq;
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
        private string HeldDirectionalButton = "";
        private string HeldBrightnessButton = "";
        public override void Initialize()
        {
            //subscribe for ikea button events
            EventChanges
                //listen for all zigbee home automation events (zhe_event)
                .Where(e => e.Event.Contains("zha_event"))
                .Subscribe(s =>
               {

                   if (s?.Data == null)
                       return;
                   double args = 0;
                   string command = "";

                   try
                   {
                       args = s.Data["args"][0];
                   }
                   catch { /*do nothing and continue*/};


                   try
                   {
                        command = s.Data["command"];
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
                           OnDownButtonPressed();
                           break;
                       //press brightness up
                       case "step_with_on_off":
                           OnUpButtonPressed();
                           break;
                       //hold brightness down
                       case "move":
                           HeldBrightnessButton = "DOWN";
                           OnUpButtonHold();
                           break;
                       // hold brightness down
                       case "move_with_on_off":
                           HeldBrightnessButton = "UP";
                           OnDownButtonHold();
                           break;
                       // release of brightness buttons
                       case "stop":
                           HandleStopEvents(args);
                           break;
                       case "toggle"://power button
                           OnPowerButtonPressed();
                           break;
                   }

               });


            //print out all events
            EventChanges
                .Subscribe(s => 
                {
                    System.Diagnostics.Debug.WriteLine($"Event = {s.Event}\n Data = {s.Data}\nDomain = {s.Domain}\n");
                });

        }

        /// <summary>
        /// Handle releasing of directional button
        /// </summary>
        private void HandleReleaseEvents(double timeHeld)
        {
            if (HeldDirectionalButton == "LEFT")
            {
                OnLeftButtonReleased();
            }
            else if (HeldDirectionalButton == "RIGHT")
            {
                OnRightButtonReleaed();
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
                OnUpButtonReleased();
            }
            else if (HeldBrightnessButton == "DOWN")
            {
                OnDownButtonReleased();
            }
            System.Diagnostics.Debug.WriteLine($"*Time {timeHeld}");

        }

        private void HandlePressEvents(double deviceId)
        {
            switch (deviceId)
            {
                case 256://right button tap
                    OnRightButtonPressed();
                    break;
                case 257://left button tap
                    OnLeftButtonPressed();
                    break;
            };
        }

        private void HandleHoldEvent(double timeHeld)
        {
            switch (timeHeld)
            {
                case 3328://right button tap
                    OnRightButtonHold();
                    HeldDirectionalButton = "RIGHT";
                    break;
                case 3329://left button tap
                    OnLeftButtonHold();
                    HeldDirectionalButton = "LEFT";
                    break;
            };
        }

        private void OnLeftButtonPressed()
        {
            Entity("switch.plug").Toggle();
            System.Diagnostics.Debug.WriteLine("on Left Button click");

        }

        private void OnLeftButtonHold()
        {
            System.Diagnostics.Debug.WriteLine("Left Button hold");
        }

        private void OnLeftButtonReleased()
        {
            System.Diagnostics.Debug.WriteLine("Left Button released");
        }

        private void OnRightButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Right Button Pressed");
        }

        private void OnRightButtonHold()
        {
            System.Diagnostics.Debug.WriteLine("Right Button hold");
        }

        private void OnRightButtonReleaed()
        {
            System.Diagnostics.Debug.WriteLine("Right Button released");
        }

        private void OnUpButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Up Button Pressed");
            Entity("switch.plug").Toggle();

        }
        private void OnUpButtonHold()
        {
            System.Diagnostics.Debug.WriteLine("Up Button hold");
        }

        private void OnUpButtonReleased()
        {
            System.Diagnostics.Debug.WriteLine("Up Button released");
        }

        private void OnDownButtonHold()
        {
            System.Diagnostics.Debug.WriteLine("Down Button hold");
        }

        private void OnDownButtonReleased()
        {
            System.Diagnostics.Debug.WriteLine("Down Button released");
        }


        private void OnDownButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Down Button Pressed");
            CallService("media_player", "media_play_pause", new { entity_id = "media_player.living_room_apple_tv" });



        }

        private void OnPowerButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("Power Button Pressed");
            NotifyMe();
            CallService("light", "toggle", 
                new { entity_id = "light.sengled_e11_n1ea_b24c0700_level_light_color_on_off" ,
                    color_name ="red",
                    brightness = "255",
                    effect = "random"});
        }

        private void NotifyMe()
        {
            CallService("notify", "notify",
                new {
                    title = "Hello Gabriel",
                    message = "This is a message"
                });
        }


    }
}
