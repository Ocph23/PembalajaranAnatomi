using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class TagItem : BaseViewModel
    {
        string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private TimeSpan position;

        public TimeSpan Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }

    }
}
