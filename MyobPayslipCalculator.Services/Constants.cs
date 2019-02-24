namespace MyobPayslipCalculator.Services
{
    public static class Constants
    {       
        public const string NoDataToWrite = "No Data To Write To CSV.";        
        public const string PressAnyKeyToQuitMessage = "Press any key to Quit Program";        
        public const string EmployeeFileNotExistsMessage = "File does not exist. Please provide path to employees csv file including file extension";        
        public const string PaySlipFilePathInvalidMessage = "Path is invalid. Please provide valid path to save payslip file.";        
        public const string EnterPaySlipFilePathMessage = "\nPlease enter file path for the output payslip csv file. Please note that the you must have permission to write file in the folder without admin access, otherwise run this program with admin privilege";        
        public const string EnterEmployeeFilePathMessage = "\nPlease enter file path for the input employees csv file.";        
        public const string PaySlipsSuccess = "{0} payslips generated.Press any key to quit.";        
        public const string FoundEmployeesMessage = "Found {0} employees in the loaded file. Processing.....";        
        public const string WelcomeMessage = "#### WELCOME to MYOB PAYSLIP SYSTEM v1.0 - Developed By Deepindera ####\n\n\n> Import CSV File for Employees will have structure like  David,Rudd,60050,9%,01 March – 31 March.  \n> There will no first row for data columns.";        
    }
}
