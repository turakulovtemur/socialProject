using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.ViewModels
{
    public class PersonViewModel
    {
        public string FirstName { get; set; } //Фамилия
        public string Name { get; set; } //Имя
        public string LastName { get; set; } //Отчетсво

        public DateTime? DateOfBirth { get; set; } //дата рождения

        public string BornPlace { get; set; } //место рождения

        public string Address { get; set; } //адрес проживания

        public string PasportSeria { get; set; } //серия паспорта

        public string PasportNumber { get; set; } //номер паспорта

        public string DepartmentCode { get; set; } //код подразделения

        public DateTime? DateOfIssue { get; set; } //дата выдачи

        public string IssueBy { get; set; } //кем выдан

        public string SexOfPerson { get; set; } //пол человека

        public string SnilsNumber { get; set; }

        public string PhoneNumber { get; set; }
    }
}
