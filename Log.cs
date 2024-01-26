using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AngleReaderWF
{
    public class LogEntry
    {
        
        public String LogText { get; set; }

        public LogEntry(String newText)
        {
            LogText = newText;
        }

    }

    public class Log : INotifyPropertyChanged
    {
        private int logLimit = 1000;
        
        public BindingList<LogEntry> _entries = new BindingList<LogEntry>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddLogEntry(LogEntry entry)
        {
            _entries.Add(entry);

            if(_entries.Count > logLimit)
            {
                _entries.RemoveAt(0);
            }

            OnPropertyChanged();
        }

        public void AddLogEntry(string text)
        {
            AddLogEntry(new LogEntry(text));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
