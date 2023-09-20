# Running the Software
1.Set Startup Project: In the Solution Explorer, right-click on the project corresponding to the MainWindow (likely named PROG_Part1) and select Set as Startup Project.
2.Run the Application: Press F5 or click on the "Start Debugging" button in Visual Studio to run the application. The MainWindow window should appear.
3.Using the Application: The MainWindow window allows you to add academic modules. Fill in the module information (Module Code, Module Name, Number of Credits, Class Hours per Week, Number of Weeks in Semester, and Start Date of Semester) and click the "Add Module" button to add a module to the list view below.
4.Proceed to Recording Study Hours: To proceed to recording study hours, click the "Next" button.
5.Recording Study Hours: In the ADD window, select a module from the drop-down list, enter the date of study, and the number of study hours. Click the "Record Info" button to record study hours for the selected module.

## MainWindow: Module Information Entry
You can enter the following information for each module:
Module Code: A unique code for the module.
Module Name: The name of the module.
Number of Credits: The credit value associated with the module.
Class Hours per Week: The number of hours of in-class lectures or sessions per week for the module.
Number of Weeks in Semester: The total number of weeks in the academic semester during which the module is taught.
Start Date of Semester: The start date of the academic semester.

## Adding Modules
 After entering the module information, you can click the "Add Module" button. This action creates a new module object with the entered details and adds it to a list of modules displayed in a ListView in the window.

 ## Viewing Modules
 The ListView displays a list of all added modules, including their codes, names, credits, class hours per week, weeks in the semester, and start dates.

 ## Proceed to Recording Study Hours
 Once you have added modules, you can click the "Next" button at the bottom of the window. This action opens the ADD window.

## ADD: Module Selection
In the ADD window, you can select a module from the drop-down list (ComboBox) to record study hours for that module.

## Date Selection
You can choose a date for which you want to record study hours using the date picker (DatePicker).

## Study Hour Entry
Enter the number of study hours for the selected module and date in the provided text box.

## Recording Study Hours
After entering the study hours and selecting a date, click the "Record Info" button. This action records the study hours for the selected module and date.

## Remaining Self-Study Hours
The application calculates and displays the remaining self-study hours for the current week for each module. The ListView in the ADD window shows this information, including the module code, module name, total recorded hours, remaining hours for the current week, and the start date of the current week.

## Clearing Input Fields
 After recording study hours, the input fields (date and study hours) are cleared to allow you to record more study hours.
























 
