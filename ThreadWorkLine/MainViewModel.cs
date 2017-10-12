using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadWorkLine
{
    public delegate void SendMsg(string message);

    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            var thread = new Thread(() => DoWorkA(Worker1));
            thread.Start();
            thread = new Thread(() => DoWorkA(Worker2));
            thread.Start();
            thread = new Thread(() => DoWorkA(Worker3));
            thread.Start();

        }

        private void DoWorkA(Worker worker)
        {
            DisplayMessage(worker, $"waiting for {wholocked} to do A");
            lock (lockA)
            {
                wholocked = worker.ToString();
                DisplayMessage(worker, "Doing A");
                Thread.Sleep(3000);
            }
            DisplayMessage(worker, "Done doing A");


        }


        private string wholocked = "";
        private static object lockA = new object();


        private Worker Worker1 = new Worker("Worker1");
        private Worker Worker2 = new Worker("Worker2");
        private Worker Worker3 = new Worker("Worker3");

        private void DisplayMessage(Worker worker, string message)
        {
            if(worker == Worker1) Thread1StateText = message;
            else if (worker == Worker2) Thread2StateText = message;
            else if (worker == Worker3) Thread3StateText = message;

        }

        public string Thread1StateText
        {
            get => _thread1StateText;
            set
            {
                if (_thread1StateText == value) return;
                _thread1StateText = value;
                OnPropertyChanged(nameof(Thread1StateText));

            }
        }

        public string Thread2StateText
        {
            get => _thread2StateText;
            set
            {
                if (_thread2StateText == value) return;
                _thread2StateText = value;
                OnPropertyChanged(nameof(Thread2StateText));

            }
        }

        public string Thread3StateText
        {
            get => _thread3StateText;
            set
            {
                if (_thread3StateText == value) return;
                _thread3StateText = value;
                OnPropertyChanged(nameof(Thread3StateText));

            }
        }


        private string _thread1StateText;
        private string _thread2StateText;
        private string _thread3StateText;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

        }
        #endregion
    }

    public class Worker
    {
        private string name;

        public Worker(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
