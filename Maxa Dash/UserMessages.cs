using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maxa_Dash
{
    class UserMessages
    {
        public UserMessages(StackPanel stackPanel)
        {
            this.stackPanel = stackPanel;
        }

        private StackPanel stackPanel;

        public void AddMessage(ref List<MessageLabel> labelslist,string message,float duration = 1f, Brush textColor = null)
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
