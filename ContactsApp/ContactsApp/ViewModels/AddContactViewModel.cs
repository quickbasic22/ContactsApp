using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ContactsApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace ContactsApp.ViewModels
{
    public class AddContactViewModel: INotifyPropertyChanged
    {
        readonly ContactsApp.Models.Contact Contact;

        public AddContactViewModel(ContactsApp.Models.Contact contact)
        {
            Contact = contact;
            Contact.Name = "David Morrow";
            Contact.Website = @"https://www.bing.com/";
            Contact.BestFriends = true;
            Contact.IsBusy = false;
            LaunchWebsiteCommand = new Command(LaunchWebsite, () => !IsBusy);
            SaveContactCommand = new Command(async ()=> await SaveContact(), () => !IsBusy);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public bool BestFriends
        {
            get 
            {
                return Contact.BestFriends;
            }
            set
            {
                Contact.BestFriends = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string Name
        {
            get
            {
                return Contact.Name;
            }
            set
            {
                Contact.Name = value;
                if (Name == "David")
                    IsBusy = true;
                else
                    IsBusy = false;

                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string DisplayMessage
        {
            get
            {
                return $"Your new friend is named {Name} and " +
                    $"{(BestFriends ? "is" : "is not")} your best friend";

            }
            
        }

        public string Website 
        { 
            get
            {
                return Contact.Website;
            }
            set
            {
                Contact.Website = value.ToString();
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public bool IsBusy
        {
            get
            {
                return Contact.IsBusy;
            }
            set
            {
                Contact.IsBusy = value;
                OnPropertyChanged();
                LaunchWebsiteCommand.ChangeCanExecute();
                SaveContactCommand.ChangeCanExecute();

            }
        }

        public Command LaunchWebsiteCommand { get; }
        public Command SaveContactCommand { get; }


        void LaunchWebsite()
        {
            try
            {
                Launcher.OpenAsync(new Uri(Website));
            }
            catch
            {

            }
        }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(4000);

            IsBusy = false;


            await Application.Current.MainPage.DisplayAlert("Save", "Contact Saved", "Close Dialog");
        }

        
        
    }
}
