using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<string, int[]> exampleOneAvailability    = new Dictionary<string, int[]>()
            //{
            //    {"Sunday", new int[] {0, 0} },
            //    {"Monday", new int[] {8, 12} },
            //    {"Tuesday", new int[] {8, 12} },
            //    {"Wednesday", new int[] {10, 15} },
            //    {"Thursday", new int[] {12, 8} },
            //    {"Friday", new int[] {8, 12} },
            //    {"Saturday", new int[] {0, 0} },
            //};
            //Dictionary<string, int[]> exampleTwoAvailability    = new Dictionary<string, int[]>()
            //{
            //    {"Sunday", new int[] {8, 22} },
            //    {"Monday", new int[] {0, 0} },
            //    {"Tuesday", new int[] {0, 0} },
            //    {"Wednesday", new int[] {12, 22} },
            //    {"Thursday", new int[] {0, 0} },
            //    {"Friday", new int[] {0, 0} },
            //    {"Saturday", new int[] {8, 22} },
            //};
            //Dictionary<string, int[]> exampleThreeAvailability  = new Dictionary<string, int[]>()
            //{
            //    {"Sunday", new int[] {10, 16} },
            //    {"Monday", new int[] {12, 20} },
            //    {"Tuesday", new int[] {12, 20} },
            //    {"Wednesday", new int[] {12, 20} },
            //    {"Thursday", new int[] {12, 20} },
            //    {"Friday", new int[] {10, 18} },
            //    {"Saturday", new int[] {10, 16} },
            //};
            //Dictionary<string, int[]> shifts                    = new Dictionary<string, int[]>()
            //{
            //    {"Sunday", new int[] {8, 22} },
            //    {"Monday", new int[] {8, 22} },
            //    {"Tuesday", new int[] {8, 22} },
            //    {"Wednesday", new int[] {8, 22} },
            //    {"Thursday", new int[] {8, 22} },
            //    {"Friday", new int[] {8, 22} },
            //    {"Saturday", new int[] {8, 22} },
            //};
            //Workforce workforce                                 = new Workforce();
            //workforce.employeeList.Add(new Employee             ("Kasey", exampleOneAvailability));
            //workforce.employeeList.Add(new Employee             ("Kylie", exampleTwoAvailability));
            //workforce.employeeList.Add(new Employee             ("Miley", exampleThreeAvailability));
            //Schedule schedule                                   = new Schedule(shifts, workforce.employeeList);

            Dictionary<string, int[]> exampleOneAvailability = new Dictionary<string, int[]>()
            {
                {"Sunday", new int[] {8, 15} },
            };

            Dictionary<string, int[]> exampleTwoAvailability = new Dictionary<string, int[]>()
            {
                {"Sunday", new int[] {8, 15} },
            };


            Dictionary<string, int[]> shifts = new Dictionary<string, int[]>()
            {
                {"Sunday", new int[] {8, 15} },
            };

            Workforce workforce = new Workforce();
            workforce.employeeList.Add(new Employee("Kasey", exampleOneAvailability));
            workforce.employeeList.Add(new Employee("Kylie", exampleTwoAvailability));
            Schedule schedule = new Schedule(shifts, workforce.employeeList);
            schedule.SetSchedule();
        }
    }
}
