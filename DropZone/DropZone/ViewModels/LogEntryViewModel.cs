﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DropZone.Annotations;

namespace DropZone.ViewModels
{
    /// <summary>
    /// The log entry view model.
    /// </summary>
    public class LogEntryViewModel : INotifyPropertyChanged
    {
        private string _jumpNumber;
        private DateTime _date;
        private string _location;
        private string _aircraft;
        private string _altitude;
        private string _manoeuvre;
        private string _freefallDelay;
        private string _totalTime;
        private string _container;
        private string _description;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntryViewModel"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogEntryViewModel()
        {
            Location = "Parakai";
            _date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the jump number.
        /// </summary>
        [NotNull]
        public string JumpNumber
        {
            get { return _jumpNumber; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_jumpNumber))
                {
                    return;
                }
                _jumpNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the date of the jump.
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value.Equals(_date))
                {
                    return;
                }
                _date = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the location of the jump.
        /// </summary>
        [NotNull]
        public string Location
        {
            get { return _location; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_location, StringComparison.Ordinal))
                {
                    return;
                }
                _location = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the aircraft.
        /// </summary>
        [NotNull]
        public string Aircraft
        {
            get { return _aircraft; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_aircraft, StringComparison.Ordinal))
                {
                    return;
                }
                _aircraft = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the altitude of the jump.
        /// </summary>
        [NotNull]
        public string Altitude
        {
            get { return _altitude; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_altitude))
                {
                    return;
                }
                _altitude = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the manoeuvre performed.
        /// </summary>
        [NotNull]
        public string Manoeuvre
        {
            get { return _manoeuvre; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_manoeuvre, StringComparison.Ordinal))
                {
                    return;
                }
                _manoeuvre = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the freefall delay.
        /// </summary>
        [NotNull]
        public string FreefallDelay
        {
            get { return _freefallDelay; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_freefallDelay))
                {
                    return;
                }
                _freefallDelay = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the total time.
        /// </summary>
        [NotNull]
        public string TotalTime
        {
            get { return _totalTime; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_totalTime))
                {
                    return;
                }
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        [NotNull]
        public string Container
        {
            get { return _container; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_container, StringComparison.Ordinal))
                {
                    return;
                }
                _container = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [NotNull]
        public string Description
        {
            get { return _description; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                if (value.Equals(_description, StringComparison.Ordinal))
                {
                    return;
                }
                _description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Called when a property is changed.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
