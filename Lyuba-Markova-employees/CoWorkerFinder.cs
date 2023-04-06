using System;
using System.Collections.Generic;
using System.Linq;

namespace Lyuba_Markova_employees
{
    /// <summary>
    /// Finds pairs of employees who have worked together.
    /// Could find all pairs or could find pair of employees who have worked 
    /// together on common projects for the longest period of time.
    /// </summary>
    internal class CoWorkerFinder
    {
        /// <summary>
        /// Finds all pairs of employees who have worked together.
        /// </summary>
        /// <param name="lstEmployees">List with data for every employee who worked on a project from date to date</param>
        /// <returns>List with all pairs of employees who have worked together</returns>
        public static List<CoWorkers> FindCoWorkers(List<EmpProj> lstEmployees)
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
        /// Finds how many days a pair of employees worked together.
        /// If pair of employees haven't worked together the return value is less than zero.
        /// </summary>
        /// <param name="emp1">Employee 1 worked on a project from date to date</param>
        /// <param name="emp2">Employee 2 worked on a project from date to date</param>
        /// <returns>Number of days a pair of employees worked together. If employees haven't worked together the return value is less than zero.</returns>
        private static int FindDaysWorkedTogether(EmpProj emp1, EmpProj emp2)
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

        /// <summary>
        /// Finds all common projects of the pair of employees who have worked together for the longest period of time.
        /// </summary>
        /// <param name="lstEmployees">List with data for every employee who worked on a project from date to date</param>
        /// <returns>List with pairs of employees who have worked together on common projects for the longest period of time</returns>
        public static IEnumerable<CoWorkers> FindLongestWorkedCoWorkers(List<EmpProj> lstEmployees)
        {
            List<CoWorkers> coWorkersByProj = FindCoWorkers(lstEmployees);

            var pairsWorkedOnCommonProj = coWorkersByProj.GroupBy(s => new { s.EmpId1, s.EmpId2 })
                .Select(g =>
                new
                {
                    pairEmpId1 = g.Key.EmpId1,
                    pairEmpId2 = g.Key.EmpId2,
                    pairSum = g.Sum(x => x.DaysWorked)
                });

            var pairWorkedLongest = pairsWorkedOnCommonProj.MaxBy(x => x.pairSum);
            
            IEnumerable<CoWorkers> longestWithProject = coWorkersByProj.Where(g => g.EmpId1 == pairWorkedLongest.pairEmpId1 && g.EmpId2 == pairWorkedLongest.pairEmpId2);

            return longestWithProject;
        }
    }
}
