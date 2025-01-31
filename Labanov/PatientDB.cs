using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labanov
{
    internal class PatientDB
    {
        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> Patients => patients;
        public PatientDB()
        {
            patients = new ObservableCollection<Patient>();
        }

        public bool AddPatient(Patient patient)
        {
            if (string.IsNullOrEmpty(patient.MedicalCardNumber) ||
                patients.Any(p => p.MedicalCardNumber == patient.MedicalCardNumber))
            {
                return false;
            }

            patients.Add(patient);
            return true;
        }
        public bool RemovePatient(Patient patient)
        {
            return patients.Remove(patient);
        }
    }
}

             
