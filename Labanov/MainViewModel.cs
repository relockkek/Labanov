using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Labanov
{

    public class MainViewModel : INotifyPropertyChanged
    {
        private Patient editPatient;
        private Patient selectedPatient;
        private PatientDB patientDB;

       
        public ObservableCollection<Patient> Patients { get; set; }

        public Patient EditPatient
        {
            get => editPatient;
            set
            {
                editPatient = value;
                OnPropertyChanged();
            }
        }
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> GenderOptions { get; set; }
        public ICommand AddPatientCommand { get; }
        public ICommand RemovePatientCommand { get; }
        public MainViewModel()
        { 
            AddPatientCommand = new CommandVM(AddPatient, CanAddPatient);
            RemovePatientCommand = new CommandVM(RemovePatient, CanRemovePatient);
            patientDB = new PatientDB();
            Patients = patientDB.Patients;
            EditPatient = new Patient();

          

            GenderOptions = new ObservableCollection<string> {"Мужской", "Женский"};
            
        }
        
        private void AddPatient()
        {
            if (patientDB.AddPatient(EditPatient))
            {
                EditPatient = new Patient();
                MessageBox.Show("Пациент успешно добавлен!");
            }
            else
            {
                MessageBox.Show("Ошибка: Номер медицинской карты должен быть уникальным.");
            }
        }

        private bool CanAddPatient()
        {
            return !string.IsNullOrWhiteSpace(EditPatient.FullName) &&
                   !string.IsNullOrWhiteSpace(EditPatient.MedicalCardNumber) &&
                   !string.IsNullOrWhiteSpace(EditPatient.Gender);
        }

        private void RemovePatient(object obj)
        {
            if (obj is Patient patient && patientDB.RemovePatient(patient))
            {
                selectedPatient = null;
                MessageBox.Show("Пациент успешно удален!");
            }
            else
            {
                MessageBox.Show("Ошибка: Не удалось удалить пациента.");
            }    
        }

        private bool CanRemovePatient(object obj)
        {
            return obj is Patient;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
     
    }

}
