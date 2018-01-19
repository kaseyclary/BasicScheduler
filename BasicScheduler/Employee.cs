using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScheduler
{
    class Employee
    {
        public Dictionary<string, int[]> availability = new Dictionary<string, int[]>();
        public Dictionary<string, int[]> schedule = new Dictionary<string, int[]>();
        int totalHours;
        public string name;

        public Employee()
        {
            availability = ToolSet.SetWeekHours();
        }

        //For testing
        public Employee(string name, Dictionary<string, int[]> availability)
        {
            this.name = name;
            this.availability = availability;
            totalHours = 0;
            initializeAvailability();
        }

        void initializeAvailability()
        {
            List<string> daysOff = new List<string>();
            foreach(KeyValuePair<string, int[]> kvp in availability)
            {
                if(kvp.Value[0] == 0 && kvp.Value[1] == 0)
                {
                    daysOff.Add(kvp.Key);
                }
            }

            foreach(string day in daysOff)
            {
                availability.Remove(day);
            }

        }

        public bool Compliance(int shiftLength)
        //Will check to make sure that the employee isn't already working today, and make sure that they aren't over hours.
        //Make sure to pass in the length of the shift being assigned to ensure that it won't take them over hours 
        {
            bool compliance = true;
            if (totalHours + shiftLength <= 40)
            {
                compliance = true;
            }
            else if(totalHours + shiftLength > 40)
            {
                compliance = false;
            }
            return compliance;
        }

        public void AddToSchedule(KeyValuePair<string, int[]> shift)
        {
            schedule.Add(shift.Key, shift.Value);
        }

    }
}
