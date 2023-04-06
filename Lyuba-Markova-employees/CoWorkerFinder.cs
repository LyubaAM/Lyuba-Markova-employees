using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lyuba_Markova_employees
{
    /// <summary>
    /// Finds all pairs of employees who have worked together on common projects.
    /// </summary>
    internal class CoWorkerFinder
    {
        /// <summary>
        /// Finds all pairs of employees who have worked together on common projects.
        /// </summary>
        /// <param name="lstEmployees"></param>
        /// <returns></returns>
        public List<CoWorkers> FindCoWorkers(List<EmpProj> lstEmployees)
        {
            List<CoWorkers> result = new List<CoWorkers>();

            for (int i = 0; i < lstEmployees.Count - 1; i++)
            {
                for (int j = i + 1; j < lstEmployees.Count; j++)
                {
                    if (lstEmployees[i].ProjectId == lstEmployees[j].ProjectId)
                    {
                        int daysWorkedTogether = FindDaysWorkedTogether(lstEmployees[i], lstEmployees[j]);

                        if (daysWorkedTogether > 0)
                        {
                            CoWorkers item = new CoWorkers();
                            if (lstEmployees[i].EmpId < lstEmployees[j].EmpId)
                            {
                                item.EmpId1 = lstEmployees[i].EmpId;
                                item.EmpId2 = lstEmployees[j].EmpId;
                            }
                            else
                            {
                                item.EmpId1 = lstEmployees[j].EmpId;
                                item.EmpId2 = lstEmployees[i].EmpId;
                            }
                            item.ProjectId = lstEmployees[i].ProjectId;
                            item.DaysWorked = daysWorkedTogether;

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Finds days a pair of employees worked together.
        /// If pair of employees haven't worked together the return value is less than zero.
        /// </summary>
        /// <param name="emp1"></param>
        /// <param name="emp2"></param>
        /// <returns></returns>
        public int FindDaysWorkedTogether(EmpProj emp1, EmpProj emp2)
        {
            DateTime maxDateFrom;
            DateTime minDateTo;
            DateTime emp1DateTo = emp1.DateTo.HasValue ? emp1.DateTo.Value : DateTime.Now;
            DateTime emp2DateTo = emp2.DateTo.HasValue ? emp2.DateTo.Value : DateTime.Now;

            if (DateTime.Compare(emp1.DateFrom, emp2.DateFrom) > 0)
                maxDateFrom = emp1.DateFrom;
            else
                maxDateFrom = emp2.DateFrom;

            if (DateTime.Compare(emp1DateTo, emp2DateTo) > 0)
                minDateTo = emp2DateTo;
            else
                minDateTo = emp1DateTo;

            return (minDateTo - maxDateFrom).Days;
        }
    }
}
