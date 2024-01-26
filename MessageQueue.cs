using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AngleReaderWF
{
    public class Message
    {

        public String MessageText { get; set; }

        public Message(String newText) 
        { 
            MessageText = newText;
        }

    }

    public class MessageQueue : INotifyPropertyChanged
    {

        public BindingList<Message> _messageQueue = new BindingList<Message>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddMessageToQueue(Message message)
        {
            _messageQueue.Add(message);

            OnPropertyChanged();
        }

        public void AddMessageToQueue(string text)
        {
            AddMessageToQueue(new Message(text));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
