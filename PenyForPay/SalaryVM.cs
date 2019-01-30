using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PenyForPay
{
    class SalaryVM : INotifyPropertyChanged
    {
        const decimal DOLLAR_PENNY_CONVERT_UNIT = 100m;
        const int SALARY_PROMOTION_FACTOR = 2;
        private string employmentLengthInput = "";
        public string EmploymentLengthInput
        {
            get { return employmentLengthInput; }
            set
            {
                //Credict melwil Stackoverflow, 2 positive integers allowed only
                Regex regex = new Regex(@"^[0-9]{0,2}$");
                if (regex.IsMatch(value))
                {
                    employmentLengthInput = value; SalaryCalculation(); SaveEstimate(); NotifyChanged();
                }
            }
        }
        private decimal totalSalary = 0m;
        public decimal TotalSalary
        {
            get { return totalSalary; }
            set { totalSalary = value; NotifyChanged(); }
        }
        private decimal lastDaySalary;
        public decimal LastDaySalary
        {
            get { return lastDaySalary; }
            set { lastDaySalary = value; NotifyChanged(); }
        }
        private string filePath = "";
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; NotifyChanged(); }
        }
        //calculate the total salary and salary for last day of employment
        public void SalaryCalculation()
        {
            decimal BaseSalary = 1m;
            decimal SumOfSalary = 1m;
            for (int i = 1; i < int.Parse(EmploymentLengthInput); i++)
            {
                BaseSalary *= SALARY_PROMOTION_FACTOR;
                SumOfSalary += BaseSalary;
            }
            TotalSalary = SumOfSalary / DOLLAR_PENNY_CONVERT_UNIT;
            LastDaySalary = BaseSalary / DOLLAR_PENNY_CONVERT_UNIT;
            if (int.Parse(EmploymentLengthInput) == 0)
            {
                TotalSalary = 0;
                LastDaySalary = 0;
            }
        }
        //save result to the file
        public void SaveEstimate()
        {
            string SavedMessage = "";
            SavedMessage += $"This is the estimate of working for {EmploymentLengthInput} days: salary for the las day is $ {LastDaySalary}," +
                $" total salary is $ {TotalSalary}." + Environment.NewLine;
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appPath, "SalaryStatement");
            FilePath = Path.Combine(folderPath, "Salary Estimate.txt");
            if (!Directory.Exists((folderPath)))
                Directory.CreateDirectory(folderPath);
            File.AppendAllText(filePath, SavedMessage);
        }
        //open and display the file
        public void OpenEstimateFile()
        {
            Process.Start(FilePath);
        }
        //as usual, reset all
        public void ResetAll()
        {
            EmploymentLengthInput = "0";
        }
        #region
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
