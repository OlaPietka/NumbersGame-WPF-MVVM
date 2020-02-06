using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Number
{
    class ViewModel : ViewModelBase
    {
        int attempts; 
        string randomNumber;
        string myNumber; 
        bool _try = false; 
        bool _setNumber = false; 
        bool _end = false;

        public ViewModel()
        {
            NewGame();

            ClickCommand = new RelayCommand( 
                (parameter) => 
                {
                    myNumber += parameter; 

                    Message = myNumber; 

                    _setNumber = true;
                },
                (parameter) =>
                {
                    return !_try;
                });

            TryCommand = new RelayCommand( 
                (obj) =>
                {
                    _try = true; 
                    attempts++; 

                    if (Convert.ToInt32(myNumber) < Convert.ToInt32(randomNumber)) 
                    {
                        Color = "LightGreen";
                        Message = "To small!"; 

                        Wait();
                    }
                    else if (Convert.ToInt32(myNumber) > Convert.ToInt32(randomNumber))
                    {
                        Color = "LightCoral";
                        Message = "To hight!";

                        Wait();
                    }
                    else if (Convert.ToInt32(myNumber) == Convert.ToInt32(randomNumber)) 
                    {
                        Message = randomNumber + " is right number! Attempts: " + attempts.ToString();
                        Color = "Black";

                        _end = true; 
                    }

                    NumberOfAttempts = "Number of attempts: " + attempts.ToString();

                    myNumber = "";

                    _setNumber = false; 
                },
                (obj) =>
                {
                    return _setNumber; 
                });

            NewGameCommand = new RelayCommand( 
                (obj) =>
                {
                    _try = false; 
                    NewGame();
                },
                (obj) =>
                {
                    return _end;
                });
        }

        private void NewGame()
        {
            attempts = 0;
            myNumber = "";

            Color = "Black";

            Message = "Try to find right number!";
            NumberOfAttempts = "Number of attempts: " + attempts.ToString();

            randomNumber = RandomNumber();

            _end = false;
        }

        private async Task Wait()
        {
            await Task.Delay(2000); 

            _try = false;
            Color = "Black";
        }

        private string RandomNumber() 
        {
            Random rnd = new Random();

            return rnd.Next(1, 100).ToString();
        }

        public string Message
        {
            private set { SetProperty(ref message, value); }
            get { return message; }
        }

        public string NumberOfAttempts
        {
            private set { SetProperty(ref numberOfAttempts, value); }
            get { return numberOfAttempts; }
        }

        public string Color
        {
            private set { SetProperty(ref color, value); }
            get { return color; }
        }

        public ICommand NewGameCommand { private set; get; }
        public ICommand ClickCommand { private set; get; }
        public ICommand TryCommand { private set; get; }

        public string message;
        public string numberOfAttempts;
        public string color;
    }
}
