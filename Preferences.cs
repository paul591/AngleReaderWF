using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AngleReaderWF
{
    /// <summary>
    /// Preferences Class to hold the settings from the device.
    /// This is mainly used in the settings dialog
    /// </summary>
    public class Preferences : INotifyPropertyChanged
    {
        int _filterDepth = 0;
        int _pulsePerRev = 0;
        int _loopInterval = 0;
        int _zeroValue = 0;
        String _comPort = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public Preferences() { }

        public int FilterDepth
        {
            get
            {
                return _filterDepth;
            }
            set
            {
                _filterDepth = value;
                OnPropertyChanged();
            }
        }

        public int ZeroValue
        {
            get
            {
                return _zeroValue;
            }
            set
            {
                _zeroValue = value;
                OnPropertyChanged();
            }
        }

        public int PulsePerRev
        {
            get
            {
                return _pulsePerRev;
            }
            set
            {
                _pulsePerRev = value;
                OnPropertyChanged();
            }
        }
        public int LoopInterval
        {
            get
            {
                return _loopInterval;
            }
            set
            {
                _loopInterval = value;
                OnPropertyChanged();
            }
        }

        public String COMPort
        {
            get
            {
                return _comPort;
            }
            set
            {
                _comPort = value;
                OnPropertyChanged();
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
