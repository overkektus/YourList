using System;

namespace YourList.Models
{
    public class Task
    {
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string Difficult { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime? Reminder { get; set; }

        public Task(bool done, string title, string note, string difficult, DateTime deadLine, DateTime? reminder = null)
        {
            this.Done = done;
            this.Title = title;
            this.Note = note;
            this.Difficult = difficult;
            this.DeadLine = deadLine;
            this.Reminder = reminder;
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
    }
}
