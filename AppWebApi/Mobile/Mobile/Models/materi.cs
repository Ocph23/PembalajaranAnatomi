using System;

namespace Mobile.Models
{
    public class materi : NotifyBase
    {
        public string KodeMateri
        {
            get { return _kodemateri; }
            set
            {
                SetProperty(ref _kodemateri, value);
            }
        }

        public string JudulMateri
        {
            get { return _judulmateri; }
            set
            {
                SetProperty(ref _judulmateri, value);
            }
        }

        private string _kodemateri;
        private string _judulmateri;
    }
}


