using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyuba_Markova_employees
{
    /// <summary>
    /// Employee worked on project from date to date.
    /// Parse data from csv file.
    /// </summary>
    internal class EmpProj
    {
        public int EmpId;
        public int ProjectId;
        public DateTime DateFrom;
        public DateTime? DateTo;

        public static EmpProj FromCsv(string csvLine, string separator)
        {
            string[] values = csvLine.Split(separator);
            EmpProj empProj = new EmpProj();
            empProj.EmpId = int.Parse(values[0]);
            empProj.ProjectId = int.Parse(values[1]);
            empProj.DateFrom = DateTime.Parse(values[2], CultureInfo.InvariantCulture);
            if (values[3].Trim().ToLower().Equals("null"))
                empProj.DateTo = null;
            else
                empProj.DateTo = DateTime.Parse(values[3], CultureInfo.InvariantCulture);

            return empProj;
        }
    }
}
