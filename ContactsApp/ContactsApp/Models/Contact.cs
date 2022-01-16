using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public bool BestFriends { get; set; }
        public bool IsBusy { get; set; }
    }
}
