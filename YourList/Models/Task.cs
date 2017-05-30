using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace YourList.Models
{
    public class Task : INotifyPropertyChanged
    {
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string Difficult { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime? ReminderTime { get; set; }
        public DateTime? ReminderDate { get; set; }
        //public DateTime? Reminder { get; set; }

        public Task(bool done, string title, string note, string difficult, DateTime deadLine,
            //DateTime? reminder = null,
            DateTime? reminderTime = null, DateTime? reminderDate = null)
        {
            Done = done;
            Title = title;
            Note = note;
            Difficult = difficult;
            DeadLine = deadLine;
            ReminderTime = reminderTime;
            ReminderDate = reminderDate;
            //this.Reminder = reminder;
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "title":
                        if (Title.Length > 10)
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                    case "Name":
                        //Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }

        internal static Task<object> Delay(object p)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
