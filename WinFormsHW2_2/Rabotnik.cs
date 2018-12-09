using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WinFormsHW2_2
{
    public class Rabotnik
    {
        // закрытые поля фамилия
        string surname;
        // название фирмы
        string companyName;
        // зарплата за полугодие(массив) 
        decimal[] salaryForHalfYear;
        // дата рождения 
        DateTime birthday;

        public string Surname { get => surname; set => surname = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string SalaryForHalfYear
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Clear();
                int count = salaryForHalfYear.Count();
                for (int i = 0; i < count; i++)
                {
                    if(i != count-1)
                        builder.Append((salaryForHalfYear[i].ToString() + ";"));
                    else
                        builder.Append(salaryForHalfYear[i].ToString());
                }
                return builder.ToString();
            }
            set
            {
                string[] temp = value.Split(';');
                int count = temp.Count();
                if (count==6)
                {
                    salaryForHalfYear = new decimal[count];
                    for (int i = 0; i < count; i++)
                    {
                        salaryForHalfYear[i] = Decimal.Parse(temp[i]);
                    }
                }
            }
        }
        public DateTime Birthday { get => birthday; set => birthday = value; }

        public Rabotnik(string surname, string companyName, decimal[] salaryForHalfYear, DateTime birthday)
        {
            this.surname = surname;
            this.companyName = companyName;
            this.salaryForHalfYear = salaryForHalfYear;
            if (birthday < DateTime.Now)
                this.birthday = birthday;
            else
                throw new Exception("Wrong input data by birthday " + surname);
        }
        // метод для определения средней зарплаты
        public decimal salaryAverage()
        {
            return salaryForHalfYear.Average();
        }

        // свойство для определения возраста
        public int GetAge()
        {
            if (DateTime.Now.Month < birthday.Month)
                return (DateTime.Now.Year - birthday.Year - 1);
            else
            {
                if (DateTime.Now.Day < birthday.Day)
                    return (DateTime.Now.Year - birthday.Year - 1);
                else
                    return (DateTime.Now.Year - birthday.Year);
            }
        }
    }
}
