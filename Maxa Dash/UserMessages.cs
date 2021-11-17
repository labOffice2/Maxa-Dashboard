using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;


/// <summary>
/// This class handles the display of messages to the user.
/// It gets a stackPanel on initialization to display messages on.
/// It handles the adding of new and removal of old messages.
/// </summary>
namespace Maxa_Dash
{
    class UserMessages
    {

        List<MessageLabel> labelslist = new List<MessageLabel>(); // holds a list of the active messages
        private StackPanel stackPanel;  // holds a reference to the stackPanel

        /// <summary>
        /// Initilizes an object of the UserMessages class
        /// </summary>
        /// <param name="stackPanel">The stackPanel to hold the messages</param>
        public UserMessages(StackPanel stackPanel)
        {
            this.stackPanel = stackPanel;
        }

        /// <summary>
        /// Adds a message to display to the user for
        /// </summary>
        /// <param name="message">The text of the message</param>
        /// <param name="duration">The duration to display the message (in minutes)</param>
        /// <param name="textColor">The color of the text (default = black)</param>
        public void AddMessage(string message,float duration = 1f, Brush textColor = null)
        {
            foreach(var item in stackPanel.Children)
            {
                Label label = (Label)item;
                if((string)label.Content == message)
                {
                    return;
                }
            }

            Label newMessage = new();
            newMessage.Content = message;
            if (textColor == null) newMessage.Foreground = Brushes.Black;
            else newMessage.Foreground = textColor;

            stackPanel.Children.Add(newMessage);
            labelslist.Add(new MessageLabel(newMessage, duration));
        }

        /// <summary>
        /// This function checks if the duration set for each active message has passed.
        /// It removes these messages from display and from the active messages list.
        /// </summary>
        public void RemoveOldMessages()
        {
            foreach (var item in labelslist.ToArray())
            {
                if (item == null) break;
                TimeSpan displayDuration = DateTime.Now - item.startTime;
                if (displayDuration > item.duration)
                {
                    if (stackPanel.Children.Contains(item.Label))
                    {
                        stackPanel.Children.Remove(item.Label);
                    }
                    labelslist.Remove(item);
                }
            }
        }

        /// <summary>
        /// Message label class
        /// This class is used to keep track of active messages and timing their removal.
        /// </summary>
        public class MessageLabel
        {
            public Label Label;
            public DateTime startTime;
            public TimeSpan duration;

            public MessageLabel(Label label,float duration)
            {
                Label = label;
                this.duration = TimeSpan.FromMinutes(duration);
                startTime = DateTime.Now;
            }
        }
    }
}
