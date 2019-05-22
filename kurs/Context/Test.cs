using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using kurs.Annotations;

namespace kurs.Context
{
    public class Test : INotifyPropertyChanged
    {
        private List<Button> _buttons;

        public List<Button> Buttons
        {
            get => _buttons;
            set
            {
                _buttons = value;
                OnPropertyChanged(nameof(Buttons));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Test()
        {
            _buttons = new List<Button>();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
