using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScheduler
{
    class Schedule
    {
        Dictionary<string, List<int[]>> shiftList         = new Dictionary<string, List<int[]>>();  //Stores the original shifts
        Dictionary<string, List<int[]>> filledShifts      = new Dictionary<string, List<int[]>>();  //Stores the shifts that have been filled
        Dictionary<string, List<int[]>> generatedShifts   = new Dictionary<string, List<int[]>>();  //Stores shifts that have been divided and not filled
        Dictionary<string, List<int[]>> unfillableShifts  = new Dictionary<string, List<int[]>>();  //Stores shifts that cannot be filled based on current workforce availability
        static Random r = new Random();

        List<Employee> employeeList = new List<Employee>();
        IEnumerable<Employee> perfectFits;                          //This will store employees that are able to fill the entire shift
        IEnumerable<Employee> availableEmployees;                   //This will store all employees that are available to fill part of the shift, but not all of it
        IEnumerable<Employee> bestFits;                             //*Derived from availableEmployees* This will store employees that only require one newly generated shift

        int shiftLength;                                            //Used to test for compliance mainly
        int shiftOpen;                                              //Declaring all variables outside of loops to save memory
        int shiftClose;
        int availOpen;
        int availClose;
        int[] shiftHours = new int[2];
        KeyValuePair<string, int[]> thisShift;
        string dayOfShift;

        public Schedule(Dictionary<string, List<int[]>> shiftList, List<Employee> employeeList)
        {
            this.shiftList = shiftList;
            this.employeeList = employeeList;
        }

        public void SetSchedule()
        {
            foreach (KeyValuePair<string, List<int[]>> shiftsByDay in shiftList)
            {
                foreach (int[] shift in shiftsByDay.Value)
                {
                    dayOfShift = shiftsByDay.Key;
                    shiftOpen = shift[0];
                    shiftClose = shift[1];
                    shiftHours[0] = shiftOpen;
                    shiftHours[1] = shiftClose;
                    shiftLength = (shiftClose - shiftOpen) + 1; //Add one to the end because the shift goes THROUGH the last number, not TO the last number
                    thisShift = new KeyValuePair<string, int[]>(dayOfShift, shiftHours);
                    fillShift(thisShift);
                } 
            }
        }

        private void fillShift(KeyValuePair<string, int[]> shift)
        {
            //Checks to see if there are any employees that can fill the entire shift. Moves them to their own collection.
            perfectFits = from employee in employeeList
                          where (employee.availability.ContainsKey(dayOfShift)          //Where the employee is available the same day as the shift 
                          && employee.Compliance(shiftLength) == true                   //And the employee matches compliance
                          && employee.availability[dayOfShift][0] <= shiftOpen          //And the employee's availability window is equal to or larger than the shift on both ends
                          && employee.availability[dayOfShift][1] >= shiftClose)
                          select employee;                                              //Move the employee to a refined list (IEnumerable)

            if (perfectFits.Count<Employee>() > 0)                                      //Checks to see if there are any employees that will fit the shift perfectly
            {
                List<Employee> fits = perfectFits.ToList<Employee>();                   //Changed fits from an IEnumerable to a List so that it can be indexed
                int rEmployee = r.Next(0, perfectFits.Count<Employee>());               //Selects a random employee from the "perfect fits" collection
                fits[rEmployee].AddToSchedule(thisShift);                               //Adds the shift to the employee's personal schedule
                filledShifts = AddShift(filledShifts, thisShift);                       //Adds the shift to the list of filled shifts ***Could be an extension of the Dictionary Class***
            }

            else if (perfectFits.Count<Employee>() == 0)
            {
                //Checks to see if there are any employees that can fill part of the shift. Moves them to their own collection.
                //Only runs if there isn't already a perfect fit for the shift
                availableEmployees = from employee in employeeList
                                     where employee.availability.ContainsKey(dayOfShift)
                                     && employee.availability[dayOfShift][0] < (shiftClose - 2) //Makes sure that the shift will be at least 3 hours long on back end
                                     && employee.availability[dayOfShift][1] > (shiftOpen + 2) //Makes sure that the shift will be at least 3 hours long on front end   
                                     select employee;

                if (availableEmployees.Count<Employee>() > 0)
                {
                    availableEmployees = availableEmployees.ToList<Employee>();
                    bestFits = from employee in availableEmployees
                               where (employee.availability[dayOfShift][0] <= shiftOpen)     //Checks to see if the employee can make the beginning of the shift
                               || (employee.availability[dayOfShift][1] >= shiftClose)       //Checks to see if the employee can make the end of the shift
                               select employee;
                }

                else
                {
                    unfillableShifts = AddShift(unfillableShifts, thisShift);
                    Console.WriteLine("It appears that no employee is available to work {0} from {1} to {2}", dayOfShift, shiftOpen, shiftClose);
                }

            }
                       
        }

        private void divideShift(KeyValuePair<string, int[]> originalShift, KeyValuePair<string, int[]> filledPortion)
        //Takes a shift that was only partially filled and generates a new shift from the unfilled portion
        {

        }

        //Adds shifts to shift dictionaries. 

        Dictionary<string, List<int[]>> newDictionary;
        List<int[]> newList;
        private Dictionary<string, List<int[]>> AddShift(Dictionary<string, List<int[]>> OriginalDictionary, KeyValuePair<string, int[]> shiftToAdd)
        {
            newDictionary = OriginalDictionary;
            if (OriginalDictionary.ContainsKey(shiftToAdd.Key))
            {
                newDictionary[shiftToAdd.Key].Add(shiftToAdd.Value);
            }
            else
            {
                newDictionary.Add(shiftToAdd.Key, new List<int[]>());
                newDictionary[shiftToAdd.Key].Add(shiftHours);
            }
            return newDictionary;
        }

    }
}
